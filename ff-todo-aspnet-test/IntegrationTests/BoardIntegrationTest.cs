using ff_todo_aspnet.Constants;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;

namespace ff_todo_aspnet_test.IntegrationTests;
public class BoardIntegrationTest
{
    private readonly HttpClient client;
    private readonly ITestOutputHelper logger;

    public BoardIntegrationTest(ITestOutputHelper logger)
    {
        var webapp = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.UseSetting("IsRealDatabase", "false"));
        client = webapp.CreateDefaultClient();
        this.logger = logger;
    }

    [Fact]
    public async Task GetBoardIds()
    {
        var response = await client.GetAsync(TodoCommon.boardPath);
        response.EnsureSuccessStatusCode();
        var responseObject = JArray.Parse(await response.Content.ReadAsStringAsync());
        var expectedObject = new JArray();
        logger.WriteLine($"GetBoardIds: {expectedObject},{responseObject}");
        Assert.Equal(expectedObject, responseObject);
    }
}
