using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace snowflake;

public class SnowflakeCRUD
{
    private readonly ILogger<SnowflakeCRUD> _logger;

    public SnowflakeCRUD(ILogger<SnowflakeCRUD> logger)
    {
        _logger = logger;
    }

    [Function("SnowflakeCRUD")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}