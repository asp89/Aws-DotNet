using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ElastiCacheApp
{
    public class ConsoleApp
    {
        Redis Redis;
        Dictionary<string, List<int>> Map;
        public ConsoleApp(Redis redis)
        {
            Redis = redis;
        }

        public async Task Run()
        {
            try
            {
                using (StreamReader r = new StreamReader("~data.json"))
                {
                    string json = r.ReadToEnd();
                    Map = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(json);
                }
                Redis.SetDictionary<List<int>>(Map);
                var dictionaryKeys = Redis.GetDictionaryKeys();
                var map = Redis.GetValuesByKeys<List<int>>(dictionaryKeys);

                Console.WriteLine(map.ElementAt(0));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
            await Task.CompletedTask;
        }
    }
}