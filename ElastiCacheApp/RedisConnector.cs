using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ElastiCacheApp
{
    public class RedisConnector
    {
        public RedisConnector()
        {

        }

        // public IDatabase GetDatabase()
        // {
        //     IDatabase databaseReturn = null;
        //     string connectionString = "learn-elasticache-redis.iuqtrz.clustercfg.use2.cache.amazonaws.com:6379";
        //     var connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
        //     if (connectionMultiplexer.IsConnected)
        //         databaseReturn = connectionMultiplexer.GetDatabase();

        //     return databaseReturn;
        // }


        // public T Get<T>(string cacheKey)
        // {
        //     return Deserialize<T>(GetDatabase().StringGet(cacheKey));
        // }

        // public object Get(string cacheKey)
        // {
        //     return Deserialize<object>(GetDatabase().StringGet(cacheKey));
        // }

        // public void Set(string cacheKey, object cacheValue)
        // {
        //     GetDatabase().StringSet(cacheKey, Serialize(cacheValue));

        // }

        // public void HashSet(object obj, string objName)
        // {
        //     GetDatabase().HashSet(objName, obj.ToHashEntries());
        // }


        // //Serialize in Redis format:
        // public HashEntry[] ToHashEntries(this object obj)
        // {
        //     PropertyInfo[] properties = obj.GetType().GetProperties();
        //     return properties
        //         .Where(x => x.GetValue(obj) != null) // <-- PREVENT NullReferenceException
        //         .Select(property => new HashEntry(property.Name, property.GetValue(obj)
        //         .ToString())).ToArray();
        // }


        // //Deserialize from Redis format
        // public T ConvertFromRedis<T>(this HashEntry[] hashEntries)
        // {
        //     PropertyInfo[] properties = typeof(T).GetProperties();
        //     var obj = Activator.CreateInstance(typeof(T));
        //     foreach (var property in properties)
        //     {
        //         HashEntry entry = hashEntries.FirstOrDefault(g => g.Name.ToString().Equals(property.Name));
        //         if (entry.Equals(new HashEntry())) continue;
        //         property.SetValue(obj, Convert.ChangeType(entry.Value.ToString(), property.PropertyType));
        //     }
        //     return (T)obj;
        // }

        // private byte[] Serialize(object obj)
        // {
        //     if (obj == null)
        //     {
        //         return null;
        //     }
        //     BinaryFormatter objBinaryFormatter = new BinaryFormatter();
        //     using (MemoryStream objMemoryStream = new MemoryStream())
        //     {
        //         objBinaryFormatter.Serialize(objMemoryStream, obj);
        //         byte[] objDataAsByte = objMemoryStream.ToArray();
        //         return objDataAsByte;
        //     }
        // }

        // private static T Deserialize<T>(byte[] bytes)
        // {
        //     BinaryFormatter objBinaryFormatter = new BinaryFormatter();
        //     if (bytes == null)
        //         return default(T);

        //     using (MemoryStream objMemoryStream = new MemoryStream(bytes))
        //     {
        //         T result = (T)objBinaryFormatter.Deserialize(objMemoryStream);
        //         return result;
        //     }
        // }

        // public static void SetComplexObject<T>(object obj, string objName)
        // {
        //     //Saving and retrieval of a single complex object
        //     var ser = new DataContractJsonSerializer(typeof(T));
        //     var objJSON = string.Empty;

        //     using (var ms = new MemoryStream())
        //     {
        //         ser.WriteObject(ms, obj);
        //         objJSON = Encoding.UTF8.GetString(ms.ToArray());
        //     }

        //     RedisConnector.Set(objName, objJSON);
        // }

        // public T GetComplexObject<T>(string objName)
        // {
        //     var ser = new DataContractJsonSerializer(typeof(T));
        //     var redisObj = RedisConnector.Get(objName);
        //     using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(redisObj.ToString())))
        //     {
        //         var processedObj = (T)ser.ReadObject(ms);
        //         return processedObj;
        //     }
        // }

        // public void SetComplexList<T>(List<T> listobj, string objName)
        // {
        //     List<string> lstStrJSON = new List<string>();
        //     foreach (T obj in listobj)
        //     {
        //         var ser = new DataContractJsonSerializer(typeof(T));
        //         var objJSON = string.Empty;

        //         using (var ms = new MemoryStream())
        //         {
        //             ser.WriteObject(ms, obj);
        //             objJSON = Encoding.UTF8.GetString(ms.ToArray());
        //         }

        //         lstStrJSON.Add(objJSON);
        //     }
        //     RedisConnector.Set(objName, lstStrJSON);
        // }

        // public List<T> GetComplexList<T>(string objName)
        // {
        //     //Getting a list of complex objects from Redis
        //     if (RedisConnector.GetDatabase() != null)
        //     {
        //         var lstObj = (List<string>)RedisConnector.Get(objName);
        //         List<T> lstRetrievedObj = new List<T>();
        //         var ser = new DataContractJsonSerializer(typeof(T));

        //         foreach (string str in lstObj)
        //         {
        //             using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(str)))
        //             {
        //                 var processedObj = (T)ser.ReadObject(ms);
        //                 lstRetrievedObj.Add(processedObj);
        //             }
        //         }
        //         return lstRetrievedObj;
        //     }
        //     else
        //         return null;
        // }
    }
}