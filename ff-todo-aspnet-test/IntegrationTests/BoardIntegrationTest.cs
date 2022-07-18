using ff_todo_aspnet.Constants;
using ff_todo_aspnet_test.Utilities;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using System.Text;
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
        var response = await client.GetAsync($"{TodoCommon.boardPath}");
        response.EnsureSuccessStatusCode();
        var expectedObject = new JArray();
        var responseObject = JArray.Parse(await response.Content.ReadAsStringAsync());
        logger.WriteLine($"GetBoardIds: {expectedObject},{responseObject}");
        Assert.Equal(expectedObject, responseObject);
    }

    [Fact]
    public async Task GetBoard()
    {
        var testBoard = TestEntityProvider.GetTestBoard();
        var testBoardRequest = TestEntityConverter.GetBoardRequest(testBoard);
        long testBoardId = 0L;
        var jsonContent = "";
        var request = await client.PutAsync($"{TodoCommon.boardPath}", new StringContent(jsonContent, Encoding.UTF8, "application/json"));
        var response = await client.GetAsync($"{TodoCommon.boardPath}/{testBoardId}");
        /*
        var expectedObject = JObject.Parse("");
        var responseObject = JObject.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(expectedObject, responseObject);
        */
    }
}
