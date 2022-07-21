using ff_todo_aspnet.Entities;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;
using ff_todo_aspnet.Services;
using ff_todo_aspnet_test.Utilities;
using Moq;
using System.Collections.ObjectModel;

namespace ff_todo_aspnet_test.ServiceUnitTests;
public class BoardServiceUnitTest
{
    private readonly Mock<IBoardService> mockService = new Mock<IBoardService>();

    [Fact]
    public void GetBoardIdsTest()
    {
        var testBoards = TestEntityProvider.GetTestBoardIds();

        mockService.Setup(s => s.GetBoardIds()).Returns(testBoards);

        var expected = testBoards;
        var actual = mockService.Object.GetBoardIds();

        Assert.Equal(expected.GetType(), actual.GetType());
        Assert.Equal(expected.Count(), actual.Count());
    }

    [Fact]
    public void GetBoardsTest()
    {
        var testBoards = TestEntityProvider.GetTestBoardResponses();

        mockService.Setup(s => s.GetBoards()).Returns(testBoards);

        var expected = testBoards;
        var actual = mockService.Object.GetBoards();

        Assert.Equal(expected.GetType(), actual.GetType());
        Assert.Equal(expected.Count(), actual.Count());
    }

    [Fact]
    public void GetExistingBoardTest()
    {
        Board testEntity = TestEntityProvider.GetTestBoard();
        long testId = 0L;

        mockService.Setup(s => s.GetBoard(testId)).Returns(testEntity);

        var expected = testEntity;
        var actual = mockService.Object.GetBoard(testId);

        Assert.NotNull(actual);
        if (actual is not null)
            TestEntityAsserter.AssertBoardResponsesEqual(expected, actual);
    }

    [Fact]
    public void GetNonExistentBoardTest()
    {
        long testId = 666L;

        mockService.Setup(s => s.GetBoard(testId)).Returns(null as BoardResponse);

        var actual = mockService.Object.GetBoard(testId);

        Assert.Null(actual);
    }

    [Fact]
    public void AddBoardTest()
    {
        Board testEntity = TestEntityProvider.GetTestBoard();
        BoardRequest testRequest = TestEntityConverter.GetBoardRequest(testEntity);

        mockService.Setup(s => s.AddBoard(testRequest)).Returns(testEntity);

        var expected = testEntity;
        var actual = mockService.Object.AddBoard(testRequest);

        TestEntityAsserter.AssertBoardResponsesEqual(expected, actual);
    }

    [Fact]
    public void UpdateExistingBoardTest()
    {
        Board testEntity = TestEntityProvider.GetTestBoard();
        Board updateTestEntity = TestEntityProvider.GetUpdateTestBoard();
        BoardRequest updateTestRequest = TestEntityConverter.GetBoardRequest(updateTestEntity);
        BoardResponse updatedTestResponse = TestEntityConverter.GetBoardResponse(updateTestRequest);
        long testId = 0L;

        mockService.Setup(s => s.UpdateBoard(testId, updateTestRequest)).Returns(updatedTestResponse);

        var expected = updatedTestResponse;
        var actual = mockService.Object.UpdateBoard(testId, updateTestRequest);

        Assert.NotNull(actual);
        if (actual is not null)
            TestEntityAsserter.AssertBoardResponsesEqual(expected, actual);
    }

    [Fact]
    public void UpdateNonExistentBoardTest()
    {
        Board updateTestEntity = TestEntityProvider.GetUpdateTestBoard();
        BoardRequest updateTestRequest = TestEntityConverter.GetBoardRequest(updateTestEntity);
        long testId = 666L;

        mockService.Setup(s => s.UpdateBoard(testId, updateTestRequest)).Returns(null as BoardResponse);

        var actual = mockService.Object.UpdateBoard(testId, updateTestRequest);

        Assert.Null(actual);
    }

    [Fact]
    public void DeleteExistingBoardTest()
    {
        Board testEntity = TestEntityProvider.GetTestBoard();
        long testId = 0L;

        mockService.Setup(s => s.RemoveBoard(testId)).Returns(testEntity);

        var expected = testEntity;
        var actual = mockService.Object.RemoveBoard(testId);

        Assert.NotNull(actual);
        if (actual is not null)
            TestEntityAsserter.AssertBoardsEqual(expected, actual);
    }

    [Fact]
    public void DeleteNonExistentBoardTest()
    {
        long testId = 666L;

        mockService.Setup(s => s.RemoveBoard(testId)).Returns(null as Board);

        var actual = mockService.Object.RemoveBoard(testId);

        Assert.Null(actual);
    }

    [Fact]
    public void GetBoardReadonlyTodosTest()
    {
        long testId = 0L;
        bool testReadonlyState = false;

        mockService.Setup(s => s.GetBoardReadonlyTodosSetting(testId)).Returns(testReadonlyState);

        var expected = testReadonlyState;
        var actual = mockService.Object.GetBoardReadonlyTodosSetting(testId);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetBoardReadonlyTasksTest()
    {
        long testId = 0L;
        bool testReadonlyState = false;

        mockService.Setup(s => s.GetBoardReadonlyTasksSetting(testId)).Returns(testReadonlyState);

        var expected = testReadonlyState;
        var actual = mockService.Object.GetBoardReadonlyTasksSetting(testId);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SetBoardReadonlyTodosTest()
    {
        long testId = 0L;
        bool testReadonlyState = false;

        mockService.Setup(s => s.SetBoardReadonlyTodosSetting(testId, testReadonlyState)).Verifiable();

        mockService.Object.SetBoardReadonlyTodosSetting(testId, testReadonlyState);

        mockService.Verify(s => s.SetBoardReadonlyTodosSetting(testId, testReadonlyState), Times.Once());
    }

    [Fact]
    public void SetBoardReadonlyTasksTest()
    {
        long testId = 0L;
        bool testReadonlyState = false;

        mockService.Setup(s => s.SetBoardReadonlyTasksSetting(testId, testReadonlyState)).Verifiable();

        mockService.Object.SetBoardReadonlyTasksSetting(testId, testReadonlyState);

        mockService.Verify(s => s.SetBoardReadonlyTasksSetting(testId, testReadonlyState), Times.Once());
    }
}