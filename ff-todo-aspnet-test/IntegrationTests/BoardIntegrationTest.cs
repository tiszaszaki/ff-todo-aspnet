using ff_todo_aspnet.Constants;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet_test.Utilities;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using Xunit.Abstractions;
using SystemTask = System.Threading.Tasks.Task;

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
    public async SystemTask GetBoardIds()
    {
        var response = await client.GetAsync($"{TodoCommon.boardPath}");
        response.EnsureSuccessStatusCode();
        var expectedObject = new JArray();
        var responseObject = JArray.Parse(await response.Content.ReadAsStringAsync());
        logger.WriteLine($"GetBoardIds: {expectedObject},{responseObject}");
        Assert.Equal(expectedObject, responseObject);
    }

    [Fact]
    public async SystemTask GetBoard()
    {
        var testBoard = TestEntityProvider.GetTestBoard();
        var testBoardRequest = TestEntityConverter.GetBoardRequest(testBoard);
        long testBoardId = -666L;
        var jsonContent = JsonConvert.SerializeObject(testBoardRequest);
        logger.WriteLine($"GetBoard-Add: {jsonContent}");
        var request = await client.PutAsync($"{TodoCommon.boardPath}", new StringContent(jsonContent, Encoding.UTF8, "application/json"));
        request.EnsureSuccessStatusCode();
        if (request is not null)
        {
            var addedBoardContent = await request.Content.ReadAsStringAsync();
            var addedBoard = JsonConvert.DeserializeObject(addedBoardContent) as Board;
            logger.WriteLine($"GetBoard-Fetch-Content: {addedBoardContent}");
            if (addedBoard is not null)
            {
                testBoardId = addedBoard.id;
                var response = await client.GetAsync($"{TodoCommon.boardPath}/{testBoardId}");
                jsonContent = JsonConvert.SerializeObject(addedBoard);
                logger.WriteLine($"GetBoard-Fetch: {jsonContent}");
                /*
                var expectedObject = JObject.Parse("");
                var responseObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                Assert.Equal(expectedObject, responseObject);
                */
            }
        }
    }
}
