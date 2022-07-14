using ff_todo_aspnet.PivotTables;
using Moq;
using static ff_todo_aspnet.PivotTables.LatestUpdateRecord;

namespace ff_todo_aspnet_test;

public class PivotServiceUnitTest
{
    private readonly Mock<IPivotService> mockService = new Mock<IPivotService>();

    public PivotResponse<ReadinessRecord> GetTestReadinessResponse()
    {
        var records = new List<ReadinessRecord>() {
            new ReadinessRecord {id = 0L, name = "pivot entity name", doneTaskCount = 0, taskCount = 1}
        };
        foreach (var e in records)
            e.doneTaskPercent = ReadinessRecord.GetPercent(e.doneTaskCount, e.taskCount);
        var result = new PivotResponse<ReadinessRecord>()
        {
            fields = PivotResponseTools.ExtractFieldsFromType(typeof(ReadinessRecord)),
            fieldDisplay = PivotResponseTools.ExtractFieldDisplayFromType(typeof(ReadinessRecord)),
            fieldOrder = PivotResponseTools.ExtractFieldOrderFromType(typeof(ReadinessRecord)),
            records = records
        };
        return result;
    }

    public PivotResponse<LatestUpdateRecord> GetTestLatestUpdateResponse()
    {
        var records = new List<LatestUpdateRecord>() {
            new LatestUpdateRecord {
                id = 0L, name = "pivot entity name",
                latestUpdated = DateTime.UtcNow, latestEvent = LatestUpdateEvent.ADD_TODO.ToString(),
                affectedId = 10L, affectedName = "pivot entity name affected by event"
            }
        };
        var result = new PivotResponse<LatestUpdateRecord>()
        {
            fields = PivotResponseTools.ExtractFieldsFromType(typeof(LatestUpdateRecord)),
            fieldDisplay = PivotResponseTools.ExtractFieldDisplayFromType(typeof(LatestUpdateRecord)),
            fieldOrder = PivotResponseTools.ExtractFieldOrderFromType(typeof(LatestUpdateRecord)),
            records = records
        };
        return result;
    }

    [Fact]
    public void GetBoardReadinessTest()
    {
        var expected = GetTestReadinessResponse();

        mockService.Setup(s => s.GetBoardReadiness()).Returns(expected);

        var actual = mockService.Object.GetBoardReadiness();

        Assert.Equal(expected.GetType(), actual.GetType());
        Assert.Equal(expected.records.GetType(), actual.records.GetType());
        Assert.Equal(expected.records.Count(), actual.records.Count());
    }

    [Fact]
    public void GetTodoReadinessTest()
    {
        var expected = GetTestReadinessResponse();

        mockService.Setup(s => s.GetTodoReadiness()).Returns(expected);

        var actual = mockService.Object.GetTodoReadiness();

        Assert.Equal(expected.GetType(), actual.GetType());
        Assert.Equal(expected.records.GetType(), actual.records.GetType());
        Assert.Equal(expected.records.Count(), actual.records.Count());
    }

    [Fact]
    public void GetBoardLatestUpdateTest()
    {
        var expected = GetTestLatestUpdateResponse();

        mockService.Setup(s => s.GetBoardLatestUpdate()).Returns(expected);

        var actual = mockService.Object.GetBoardLatestUpdate();

        Assert.Equal(expected.GetType(), actual.GetType());
        Assert.Equal(expected.records.GetType(), actual.records.GetType());
        Assert.Equal(expected.records.Count(), actual.records.Count());
    }

    [Fact]
    public void GetTodoLatestUpdateTest()
    {
        var expected = GetTestLatestUpdateResponse();

        mockService.Setup(s => s.GetTodoLatestUpdate()).Returns(expected);

        var actual = mockService.Object.GetTodoLatestUpdate();

        Assert.Equal(expected.GetType(), actual.GetType());
        Assert.Equal(expected.records.GetType(), actual.records.GetType());
        Assert.Equal(expected.records.Count(), actual.records.Count());
    }
}
