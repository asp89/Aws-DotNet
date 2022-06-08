using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

namespace ElastiCacheApp
{
    public class Store
    {
        Util Util;
        ServiceStackRedis ServiceStackRedis;
        Dictionary<string, List<int>> Map;

        public Store(ServiceStackRedis serviceStackRedis, Util upc)
        {
            ServiceStackRedis = serviceStackRedis;
            Util = upc;
        }

        public async void Start()
        {
            TryLoadLastMap();

            while (true)
            {
                try
                {
                    RefreshMap();
                    TrySaveLastMap();
                }
                catch (Exception e)
                {
                    Console.WriteLine("refresh product map");
                    Console.WriteLine(e.ToString());
                }
                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }

        void TryLoadLastMap()
        {
            try
            {
                if (ServiceStackRedis.GetCount() > 0)
                {
                    var keys = ServiceStackRedis.GetDictionaryKeys();
                    var map = ServiceStackRedis.GetValuesByKeys<List<int>>(keys);
                    SetMap(map);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Action- refresh product map");
                Console.WriteLine(e.ToString());
            }
        }

        void RefreshMap()
        {
            if (Environment.GetEnvironmentVariable("ProductStore.NeverRefresh") != null)
            {
                if (Map == null)
                {
                    Dictionary<string, List<int>> emptyMap = new Dictionary<string, List<int>>();
                    SetMap(emptyMap);
                }
            }

            // don't keep running the query while developing
            if (Map != null && Environment.GetEnvironmentVariable("ProductStore.NoRefresh") != null)
                return;

            Dictionary<string, List<int>> map = new Dictionary<string, List<int>>();
            void addItem(string upc, int productId)
            {
                if (IsValidUpc(upc))
                {
                    if (productId == 0)
                    {
                        // null productId -> if upc is not present, add upc with empty list
                        if (!map.ContainsKey(upc))
                            map.Add(upc, new List<int>());
                    }
                    else
                    {
                        // not null productId
                        if (map.TryGetValue(upc, out List<int> list))
                        {
                            // if product not present, add productid to the list
                            if (!list.Contains(productId))
                                list.Add(productId);
                        }
                        else
                        {
                            // add upc with list containing the single productId
                            map.Add(upc, new List<int> { productId });
                        }
                    }
                }
            }
            Console.WriteLine("init reload of product map");
            ReadItems(addItem);
            SetMap(map);
        }

        void ReadItems(Action<string, int> onRow)
        {
            var tempData = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(File.ReadAllText(@"~data.json"));
            foreach(var item in tempData)
            {
                foreach(var i in item.Value)
                {
                    onRow(item.Key, i);
                }
            }
        }

        void TrySaveLastMap()
        {
            try
            {
                ServiceStackRedis.SetDictionary<List<int>>(Map);
            }
            catch (Exception e)
            {
                Console.WriteLine("save cached product map");
                Console.WriteLine(e.ToString());
            }
        }

        void SetMap(Dictionary<string, List<int>> map)
        {
            if (map == null)
            {
                Console.WriteLine("SetMap called with null value");
            }
            else
            {
                Map = map;
                Console.WriteLine("Product map refreshed");

                // wake up waiters
                lock (this)
                    Monitor.PulseAll(this);
            }
        }

        bool IsValidUpc(string code)
        {
            return Util.IsValid(code);
        }
    }
}