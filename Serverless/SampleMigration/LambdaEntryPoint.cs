using Microsoft.AspNetCore.Hosting;
using Amazon.Lambda.AspNetCoreServer;

namespace SampleMigration
{
    public class LambdaEntryPoint: APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder
                .UseLambdaServer()
                .UseStartup<Startup>();
        }
    }
}