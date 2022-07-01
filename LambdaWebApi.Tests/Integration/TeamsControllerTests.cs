using System.Text.Json;
using Xunit;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;
using System.Reflection;

namespace LambdaWebApi.Tests.Integration;

public class TeamsControllerTests
{
    [Fact]
    [Trait("Category", "Integration")]
    public async Task Test()
    {
        var sportType = Guid.NewGuid().ToString();
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        var lambdaFunction = new LambdaEntryPoint();

        var requestStr = GetRequestString("LambdaWebApi.Tests.Integration.SampleRequests.TeamsController-Post.json");

        requestStr = requestStr.Replace("{sportType}", sportType);

        var request = JsonSerializer.Deserialize<APIGatewayProxyRequest>(requestStr, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        var context = new TestLambdaContext();
        var response = await lambdaFunction.FunctionHandlerAsync(request, context);

        Assert.Equal(200, response.StatusCode);

        requestStr = GetRequestString("LambdaWebApi.Tests.Integration.SampleRequests.TeamsController-Get.json");
        requestStr = requestStr.Replace("{sportType}", sportType);
        request = JsonSerializer.Deserialize<APIGatewayProxyRequest>(requestStr, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        response = await lambdaFunction.FunctionHandlerAsync(request, context);
        var expectedBody = $"[{{\"sportType\":\"{sportType}\",\"teamName\":\"IPF\"}}]";

        Assert.Equal(200, response.StatusCode);
        Assert.Equal(expectedBody, response.Body);
    }

    private string GetRequestString(string resourceName)
    {
        var resourcePath = Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(x => x == resourceName);
        using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath))
        using (StreamReader reader = new StreamReader(stream))
        {
            return reader.ReadToEnd();
        }
    }

}