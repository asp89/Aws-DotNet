using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using ServiceStack.Redis;

namespace ElastiCacheApp
{
    public class Redis
    {
        private const string redisConnectionString = "localhost:6379";

        public Redis()
        {

        }

        public void SetDictionary<T>(Dictionary<string, T> map)
        {
            try
            {
                using (RedisClient redisClient = new RedisClient(redisConnectionString))
                {
                    redisClient.SetAll<T>(map);
                    Console.WriteLine("---Set All Executed---");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public List<string> GetDictionaryKeys()
        {
            var result = new List<string>();
            try
            {
                using (RedisClient redisClient = new RedisClient(redisConnectionString))
                {
                    result = redisClient.GetAllKeys();
                    Console.WriteLine("---Fetched All Keys---");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return result;
        }

        public Dictionary<string, T> GetValuesByKeys<T>(List<string> keys)
        {
            // Dictionary<string, T> GetValuesMap<T>
            // var result = new Dictionary<string, List<int>>();
            var result = new Dictionary<string, T>();
            try
            {
                using (RedisClient redisClient = new RedisClient(redisConnectionString))
                {                    
                    result = redisClient.GetValuesMap<T>(keys);
                    Console.WriteLine("---Fetched All Keys and Values---");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return result;
        }
    }
}