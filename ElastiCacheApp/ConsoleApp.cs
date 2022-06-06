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
        Dictionary<string, List<int>> Map;
        public ConsoleApp()
        {
            
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

                RedisConnector.HashSet(Map,"dummy-data-1");
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