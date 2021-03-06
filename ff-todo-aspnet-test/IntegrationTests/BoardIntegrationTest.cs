using ff_todo_aspnet.Constants;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet.ResponseObjects;
using ff_todo_aspnet_test.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using Xunit.Abstractions;
using SystemTask = System.Threading.Tasks.Task;

namespace ff_todo_aspnet_test.IntegrationTests;
public class BoardIntegrationTest : IClassFixture<BoardIntegrationTestFixture>
{
    private readonly ITestOutputHelper logger;
    private readonly BoardIntegrationTestFixture fixture;

    public BoardIntegrationTest(BoardIntegrationTestFixture fixture, ITestOutputHelper logger)
    {
        this.logger = logger;
        this.fixture = fixture;
    }

    [Fact]
    public async SystemTask GetBoardIds()
    {
        var response = await fixture.client.GetAsync($"{TodoCommon.boardPath}");
        response.EnsureSuccessStatusCode();
        var expectedObject = new JArray();
        var responseObject = JArray.Parse(await response.Content.ReadAsStringAsync());
        logger.WriteLine($"GetBoardIds: {expectedObject},{responseObject}");
        Assert.Equal(expectedObject, responseObject);
    }

    [Fact]
    public async SystemTask AddValidBoard()
    {
        var testBoard = TestEntityProvider.GetTestBoard();
        var jsonContent = JsonConvert.SerializeObject(TestEntityConverter.GetBoardRequest(testBoard));
        var request = await fixture.client.PutAsync($"{TodoCommon.boardPath}", new StringContent(jsonContent, Encoding.UTF8, "application/json"));
        fixture.waitForAddOperation = false;
        request.EnsureSuccessStatusCode();
        var addedBoardContent = await request.Content.ReadAsStringAsync();
        var addedBoard = JsonConvert.DeserializeObject<Board>(addedBoardContent);
        fixture.testBoardId = addedBoard.id;
        logger.WriteLine($"AddValidBoard-Expected: {jsonContent}");
        logger.WriteLine($"AddValidBoard-Actual: {addedBoardContent}");
        TestEntityAsserter.AssertBoardsEqual(testBoard, addedBoard);
    }

    [Fact]
    public async SystemTask AddInvalidBoard()
    {
        var idx = 0;
        foreach (var testBoard in TestEntityProvider.GetInvalidBoardRequests())
        {
            var jsonContent = JsonConvert.SerializeObject(testBoard);
            var request = await fixture.client.PutAsync($"{TodoCommon.boardPath}", new StringContent(jsonContent, Encoding.UTF8, "application/json"));
            logger.WriteLine($"AddInvalidBoard-{++idx}: {jsonContent}");
        }
    }

    [Fact]
    public async SystemTask GetExistingBoard()
    {
        while (fixture.waitForAddOperation)
            await SystemTask.Delay(1);
        var testBoardId = fixture.testBoardId;
        logger.WriteLine($"GetBoard-Fetch-ID: {testBoardId}");
        var response = await fixture.client.GetAsync($"{TodoCommon.boardPath}/{testBoardId}");
        response.EnsureSuccessStatusCode();
        var fetchedBoardContent = await response.Content.ReadAsStringAsync();
        var fetchedBoard = JsonConvert.DeserializeObject<BoardResponse>(fetchedBoardContent);
        logger.WriteLine($"GetBoard-Fetch-Object: {fetchedBoardContent}");
        Assert.NotNull(fetchedBoard);
    }
}
