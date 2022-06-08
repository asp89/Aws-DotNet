using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ElastiCacheApp
{
    public class StackExchangeRedis
    {
        public StackExchangeRedis()
        {
            
        }
        public IDatabase GetDatabase()
        {
            IDatabase databaseReturn = null;
            string connectionString = "localhost:6379";
            var connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
            if (connectionMultiplexer.IsConnected)
                databaseReturn = connectionMultiplexer.GetDatabase();

            return databaseReturn;
        }

        
    }
}