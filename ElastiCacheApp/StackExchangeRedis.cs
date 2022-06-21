using StackExchange.Redis;
using System.Threading.Tasks;

namespace ElastiCacheApp
{
    public class StackExchangeRedis
    {
        public StackExchangeRedis()
        {

        }
        private IDatabase GetDatabase()
        {
            IDatabase database = null;
            string connectionString = "localhost:6379";
            var connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
            if (connectionMultiplexer.IsConnected)
                database = connectionMultiplexer.GetDatabase();

            return database;
        }

        public async Task SetAsync(string key, string value)
        {
            IDatabase redisClient = GetDatabase();
            if (redisClient != null)
            {
                await redisClient.StringSetAsync(key, value);
            }
        }

        public async Task<string> GetAsync(string key)
        {
            string result = string.Empty;
            IDatabase redisClient = GetDatabase();
            if (redisClient != null)
            {
                result = await redisClient.StringGetAsync(key);
            }
            return result;
        }

        public async Task DeleteAsync(string key)
        {
            IDatabase redisClient = GetDatabase();
            if (redisClient != null)
            {
                await redisClient.KeyDeleteAsync(key);
            }
        }
    }
}