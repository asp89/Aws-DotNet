using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using CommonLibrary.AWS.Interfaces;

namespace CommonLibrary.AWS;

public class DynamoDBService : IDynamoDBService
{
    private readonly IAmazonDynamoDB _dynamoDBClient;

    public DynamoDBService(IAmazonDynamoDB dynamoDBClient)
    {
        _dynamoDBClient = dynamoDBClient;
    }

    public async Task CreateTable(
        string tableName,
        List<KeySchemaElement> keySchema,
        List<AttributeDefinition> attributeDefinitions
    )
    {
        CreateTableRequest request = new CreateTableRequest
        {
            TableName = tableName,
            KeySchema = keySchema,
            AttributeDefinitions = attributeDefinitions,
            ProvisionedThroughput = new ProvisionedThroughput
            {
                ReadCapacityUnits = 5,
                WriteCapacityUnits = 5
            }
        };

        await _dynamoDBClient.CreateTableAsync(request);
    }

    public async Task<bool> CheckIfTableExists(string tableName)
    {
        try
        {
            var response = await _dynamoDBClient.DescribeTableAsync(tableName);
            return response != null;
        }
        catch (ResourceNotFoundException)
        {
            return false;
        }
    }

    public async Task PutItem(string tableName, Dictionary<string, AttributeValue> item)
    {
        var request = new PutItemRequest { TableName = tableName, Item = item };

        await _dynamoDBClient.PutItemAsync(request);
    }

    public async Task UpdateItem(
        string tableName,
        Dictionary<string, AttributeValue> key,
        Dictionary<string, AttributeValueUpdate> updates
    )
    {
        var request = new UpdateItemRequest
        {
            TableName = tableName,
            Key = key,
            AttributeUpdates = updates
        };

        await _dynamoDBClient.UpdateItemAsync(request);
    }

    public async Task DeleteItem(string tableName, Dictionary<string, AttributeValue> key)
    {
        var request = new DeleteItemRequest { TableName = tableName, Key = key };

        await _dynamoDBClient.DeleteItemAsync(request);
    }

    public async Task<Dictionary<string, AttributeValue>> GetItem(
        string tableName,
        Dictionary<string, AttributeValue> key
    )
    {
        var request = new GetItemRequest { TableName = tableName, Key = key };

        var response = await _dynamoDBClient.GetItemAsync(request);
        return response.Item;
    }

    public async Task<List<Dictionary<string, AttributeValue>>> ParallelScan(string tableName)
    {
        const int totalSegments = 4;
        var items = new ConcurrentBag<Dictionary<string, AttributeValue>>();
        var tasks = new List<Task>();

        for (int segmentNumber = 0; segmentNumber < totalSegments; segmentNumber++)
        {
            tasks.Add(ScanSegmentAsync(tableName, totalSegments, segmentNumber, items));
        }

        await Task.WhenAll(tasks);

        return items.ToList();
    }

    private async Task ScanSegmentAsync(
        string tableName,
        int totalSegments,
        int segmentNumber,
        ConcurrentBag<Dictionary<string, AttributeValue>> items
    )
    {
        ScanRequest request = new ScanRequest
        {
            TableName = tableName,
            TotalSegments = totalSegments,
            Segment = segmentNumber
        };

        var result = await _dynamoDBClient.ScanAsync(request);
        var segmentItems = result.Items;

        foreach (var item in segmentItems)
        {
            items.Add(item);
        }
    }
}
