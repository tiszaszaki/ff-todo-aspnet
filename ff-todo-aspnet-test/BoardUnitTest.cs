using ff_todo_aspnet.Entities;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;
using ff_todo_aspnet.Services;
using Moq;
using System.Collections.ObjectModel;

namespace ff_todo_aspnet_test;
public class BoardUnitTest
{
    private Mock<IBoardService> mockService = new Mock<IBoardService>();

    private Board GetTestBoard()
    {
        return new Board
        {
            name = "Test board",
            description = "Test description",
            author = "Test author"
        };
    }
    private Board GetUpdateTestBoard()
    {
        return new Board
        {
            name = "Updated test board",
            description = "Updated test description",
            author = "Updated test author"
        };
    }

    private Collection<BoardResponse> GetTestBoardResponses()
    {
        var boards = new Collection<BoardResponse>();
        boards.Add(GetTestBoard());
        return boards;
    }
    private Collection<long> GetTestBoardIds()
    {
        var boardIds = new Collection<long> {
            1L, 2L
        };
        return boardIds;
    }

    private BoardRequest GetBoardRequest(Board board)
    {
        return new BoardRequest
        {
            name = board.name,
            description = board.description,
            author = board.author
        };
    }
    private BoardResponse GetBoardResponse(BoardRequest board)
    {
        return new BoardResponse
        {
            name = board.name,
            description = board.description,
            author = board.author
        };
    }

    private void AssertBoardResponsesEqual(BoardResponse expected, BoardResponse actual, bool is_strict = false)
    {
        if (is_strict) Assert.Equal(expected.id, actual.id);
        Assert.Equal(expected.name, actual.name);
        Assert.Equal(expected.description, actual.description);
        Assert.Equal(expected.author, actual.author);
        if (is_strict) Assert.Equal(expected.dateCreated, actual.dateCreated);
        Assert.Equal(expected.readonlyTodos, actual.readonlyTodos);
        Assert.Equal(expected.readonlyTasks, actual.readonlyTasks);
    }
    private void AssertBoardsEqual(Board expected, Board actual, bool is_strict = false)
    {
        Assert.Equal(expected.name, actual.name);
        Assert.Equal(expected.description, actual.description);
        Assert.Equal(expected.author, actual.author);
        if (is_strict) Assert.Equal(expected.dateCreated, actual.dateCreated);
        Assert.Equal(expected.readonlyTodos, actual.readonlyTodos);
        Assert.Equal(expected.readonlyTasks, actual.readonlyTasks);
    }

    [Fact]
    public void GetBoardIdsTest()
    {
        var testBoards = GetTestBoardIds();

        mockService.Setup(s => s.GetBoardIds()).Returns(testBoards);

        var expected = testBoards;
        var actual = mockService.Object.GetBoardIds();

        Assert.Equal(expected.GetType(), actual.GetType());
        Assert.Equal(expected.Count(), actual.Count());
    }

    [Fact]
    public void GetBoardsTest()
    {
        var testBoards = GetTestBoardResponses();

        mockService.Setup(s => s.GetBoards()).Returns(testBoards);

        var expected = testBoards;
        var actual = mockService.Object.GetBoards();

        Assert.Equal(expected.GetType(), actual.GetType());
        Assert.Equal(expected.Count(), actual.Count());
    }

    [Fact]
    public void GetExistingBoardTest()
    {
        Board testEntity = GetTestBoard();
        long testId = 0L;

        mockService.Setup(s => s.GetBoard(testId)).Returns(testEntity);

        var expected = testEntity;
        var actual = mockService.Object.GetBoard(testId);

        Assert.NotNull(actual);
        if (actual is not null)
            AssertBoardResponsesEqual(expected, actual);
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
        Board testEntity = GetTestBoard();
        BoardRequest testRequest = GetBoardRequest(testEntity);

        mockService.Setup(s => s.AddBoard(testRequest)).Returns(testEntity);

        var expected = testEntity;
        var actual = mockService.Object.AddBoard(testRequest);

        AssertBoardsEqual(expected, actual);
    }

    [Fact]
    public void UpdateExistingBoardTest()
    {
        Board testEntity = GetTestBoard();
        Board updateTestEntity = GetUpdateTestBoard();
        BoardRequest updateTestRequest = GetBoardRequest(updateTestEntity);
        BoardResponse updatedTestResponse = GetBoardResponse(updateTestRequest);
        long testId = 0L;

        mockService.Setup(s => s.UpdateBoard(testId, updateTestRequest)).Returns(updatedTestResponse);

        var expected = updatedTestResponse;
        var actual = mockService.Object.UpdateBoard(testId, updateTestRequest);

        Assert.NotNull(actual);
        if (actual is not null)
            AssertBoardResponsesEqual(expected, actual);
    }

    [Fact]
    public void UpdateNonExistentBoardTest()
    {
        Board updateTestEntity = GetUpdateTestBoard();
        BoardRequest updateTestRequest = GetBoardRequest(updateTestEntity);
        long testId = 666L;

        mockService.Setup(s => s.UpdateBoard(testId, updateTestRequest)).Returns(null as BoardResponse);

        var actual = mockService.Object.UpdateBoard(testId, updateTestRequest);

        Assert.Null(actual);
    }

    [Fact]
    public void DeleteExistingBoardTest()
    {
        Board testEntity = GetTestBoard();
        long testId = 0L;

        mockService.Setup(s => s.RemoveBoard(testId)).Returns(testEntity);

        var expected = testEntity;
        var actual = mockService.Object.RemoveBoard(testId);

        Assert.NotNull(actual);
        if (actual is not null)
            AssertBoardsEqual(expected, actual);
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