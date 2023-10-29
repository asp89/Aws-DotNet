using System;
using System.Collections.Generic;
using ServiceStack.Redis;

namespace ElastiCacheApp
{
    public class ServiceStackRedis
    {
        private const string redisConnectionString = "localhost:6379";
        private RedisClient redisClient;

        public ServiceStackRedis()
        {

        }

        public string GetClient()
        {
            var client = string.Empty;
            try
            {
                using (redisClient = new RedisClient(redisConnectionString))
                {
                    client = redisClient.GetClient();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return client;
        }

        public string GetConfig()
        {
            var config = string.Empty;
            try
            {
                using (redisClient = new RedisClient(redisConnectionString))
                {
                    config = redisClient.GetConfig(redisConnectionString);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return config;
        }

        public int GetCount()
        {
            int result = 0;
            try
            {
                result = GetDictionaryKeys().Count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return result;
        }

        public void SetDictionary<T>(Dictionary<string, T> map)
        {
            try
            {
                using (redisClient = new RedisClient(redisConnectionString))
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
                using (redisClient = new RedisClient(redisConnectionString))
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
            var result = new Dictionary<string, T>();
            try
            {
                using (redisClient = new RedisClient(redisConnectionString))
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
    
        public void DeleteAllKeys<T>()
        {
            try
            {
                using (redisClient = new RedisClient(redisConnectionString))
                {
                    redisClient.DeleteAll<T>();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void DeleteAllByKeys(List<string> keys)
        {
            using (redisClient = new RedisClient(redisConnectionString))
            {
                redisClient.RemoveAll(keys);
            }
        }
    }
}