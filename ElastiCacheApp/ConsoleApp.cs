using System;
using System.Threading.Tasks;

namespace ElastiCacheApp
{
    public class ConsoleApp
    {
        ServiceStackRedis ServiceStackRedis;
        Store Store;
        public ConsoleApp(ServiceStackRedis serviceStackredis, Store store)
        {
            ServiceStackRedis = serviceStackredis;
            Store = store;
        }

        public async Task Run()
        {
            try
            {
                Store.Start();                
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