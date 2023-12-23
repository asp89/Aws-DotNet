using Microsoft.AspNetCore.Mvc;
using CommonLibrary.AWS.Interfaces;

namespace DynamoDb.API.Controllers;

[ApiController]
[Route("dynamodb/")]
public class DynamoDbController : Controller
{
    private readonly ILogger<DynamoDbController> _logger;
    private readonly IDynamoDBService _dynamoDBService;

    public DynamoDbController(ILogger<DynamoDbController> logger, IDynamoDBService dynamoDBService)
    {
        _logger = logger;
        _dynamoDBService = dynamoDBService;
    }

    [HttpGet("")]
    public string GetHealthcheck()
    {
        return "Healthcheck: Healthy";
    }

    [HttpGet]
    [Route("getall/{tableName}")]
    public async Task<IActionResult> GetAllRecords(string tableName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                return BadRequest("Table name is required.");
            }
            var records = await _dynamoDBService.ParallelScan(tableName);
            return Ok(records);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating table: {tableName}.", tableName);
            return StatusCode(500, "An internal server error occurred.");
        }
    }
}
