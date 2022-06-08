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
        ServiceStackRedis ServiceStackRedis;
        Store Store;
        // Dictionary<string, List<int>> Map;
        public ConsoleApp(ServiceStackRedis serviceStackredis, Store store)
        {
            ServiceStackRedis = serviceStackredis;
            Store = store;
        }

        public async Task Run()
        {
            try
            {
                // Remove all the keys
                ServiceStackRedis.DeleteAllKeys<Task>();

                // Start the process to save all the keys
                Store.Start();

                // Deserialise the json file.
                // Map = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(File.ReadAllText(@"~data.json"));

                // Set the data in redis.
                // ServiceStackRedis.SetDictionary<List<int>>(Map);

                // Fetch the keys.
                // var keys = ServiceStackRedis.GetDictionaryKeys();

                // Fetch all key-value pairs by keys.
                // var allKeysValues = ServiceStackRedis.GetValuesByKeys<List<int>>(keys);
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