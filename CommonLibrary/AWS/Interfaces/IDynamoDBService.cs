using System.Collections.Concurrent;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace CommonLibrary.AWS.Interfaces;

public interface IDynamoDBService
{
    Task<bool> CheckIfTableExists(string tableName);
    Task PutItem(string tableName, Dictionary<string, AttributeValue> item);
    Task DeleteItem(string tableName, Dictionary<string, AttributeValue> key);

    Task CreateTable(
        string tableName,
        List<KeySchemaElement> keySchema,
        List<AttributeDefinition> attributeDefinitions
    );

    Task UpdateItem(
        string tableName,
        Dictionary<string, AttributeValue> key,
        Dictionary<string, AttributeValueUpdate> updates
    );

    Task<Dictionary<string, AttributeValue>> GetItem(
        string tableName,
        Dictionary<string, AttributeValue> key
    );

    Task<List<Dictionary<string, AttributeValue>>> ParallelScan(string tableName);
}
