using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace ElastiCacheApp
{
    public class Store
    {
        StackExchangeRedis StackExchangeRedis;
        Dictionary<string, List<int>> Map;
        private const string cacheKey = "Map";

        public Store(StackExchangeRedis stackExchangeRedis)
        {
            StackExchangeRedis = stackExchangeRedis;
            Map = new Dictionary<string, List<int>>();
            // Map = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(File.ReadAllText(@"~data.json"));
        }

        public async void Start()
        {
            while (true)
            {
                try
                {
                    TryLoadLastMap();
                    TrySaveLastMap();
                }
                catch (Exception e)
                {
                    Console.WriteLine("refresh product map");
                    Console.WriteLine(e.ToString());
                }
                await Task.Delay(TimeSpan.FromMinutes(5));
            }
        }

        async void TryLoadLastMap()
        {
            try
            {
                var cachkeyValue = await StackExchangeRedis.GetAsync(cacheKey);
                if(!String.IsNullOrEmpty(cachkeyValue))
                {
                    var map = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(cachkeyValue);;
                    SetMap(map);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Action- refresh product map");
                Console.WriteLine(e.ToString());
            }
        }

        async void TrySaveLastMap()
        {
            try
            {
                if (Map.Count > 0)
                {
                    Console.WriteLine("---Saving value as a string---");
                    var map = JsonConvert.SerializeObject(Map, Formatting.Indented);
                    await StackExchangeRedis.SetAsync(cacheKey, map);
                }
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
    }
}