using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace ElastiCacheApp
{
    public class ConsoleApp
    {
        RedisConnector RedisConnector;
        Dictionary<string, List<int>> Map;
        public ConsoleApp(RedisConnector rdc)
        {
            RedisConnector = rdc;
        }

        public async Task Run()
        {
            try
            {
                var obj = new object { };
                using (StreamReader r = new StreamReader("~data.json"))
                {
                    string json = r.ReadToEnd();
                    Map = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(json);
                }

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