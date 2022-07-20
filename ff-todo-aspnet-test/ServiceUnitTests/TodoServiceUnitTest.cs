using ff_todo_aspnet.Constants;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;
using ff_todo_aspnet.Services;
using ff_todo_aspnet_test.Utilities;
using Moq;
using System.Collections.ObjectModel;

namespace ff_todo_aspnet_test.ServiceUnitTests;
public class TodoServiceUnitTest
{
    private readonly Mock<ITodoService> mockService = new Mock<ITodoService>();

    private readonly long defaultBoardId = -666L;

    private Collection<TodoResponse> GetTestTodoResponses()
    {
        var todos = new Collection<TodoResponse>();
        todos.Add(TestEntityProvider.GetTestTodo());
        return todos;
    }

    private void AssertTodoResponsesEqual(TodoResponse expected, TodoResponse actual, bool is_strict = false)
    {
        if (is_strict) Assert.Equal(expected.id, actual.id);
        Assert.Equal(expected.name, actual.name);
        Assert.Equal(expected.description, actual.description);
        Assert.Equal(expected.phase, actual.phase);
        if (is_strict)
        {
            Assert.Equal(expected.dateCreated, actual.dateCreated);
            Assert.Equal(expected.dateModified, actual.dateModified);
        }
        Assert.Equal(expected.deadline, actual.deadline);
        Assert.Equal(expected.boardId, actual.boardId);
    }
    private void AssertTodosEqual(Todo expected, Todo actual, bool is_strict = false)
    {
        if (is_strict) Assert.Equal(expected.id, actual.id);
        Assert.Equal(expected.name, actual.name);
        Assert.Equal(expected.description, actual.description);
        Assert.Equal(expected.phase, actual.phase);
        if (is_strict)
        {
            Assert.Equal(expected.dateCreated, actual.dateCreated);
            Assert.Equal(expected.dateModified, actual.dateModified);
        }
        Assert.Equal(expected.deadline, actual.deadline);
        Assert.Equal(expected.boardId, actual.boardId);
    }

    [Fact]
    public void GetTodosFromExistingBoardTest()
    {
        var testTodos = GetTestTodoResponses();
        var testId = 0L;

        mockService.Setup(s => s.GetAllTodosFromBoard(testId)).Returns(testTodos);

        var expected = testTodos;
        var actual = mockService.Object.GetAllTodosFromBoard(testId);

        Assert.Equal(expected.GetType(), actual.GetType());
        Assert.Equal(expected.Count(), actual.Count());
    }

    [Fact]
    public void GetTodosFromExistentBoardTest()
    {
        var noTodos = new Collection<TodoResponse>();
        var testId = 666L;

        mockService.Setup(s => s.GetAllTodosFromBoard(testId)).Returns(noTodos);

        var expected = noTodos;
        var actual = mockService.Object.GetAllTodosFromBoard(testId);

        Assert.Equal(expected.GetType(), actual.GetType());
        Assert.Equal(expected.Count(), actual.Count());
    }

    [Fact]
    public void GetTodosTest()
    {
        var testTodos = GetTestTodoResponses();

        mockService.Setup(s => s.GetTodos()).Returns(testTodos);

        var expected = testTodos;
        var actual = mockService.Object.GetTodos();

        Assert.Equal(expected.GetType(), actual.GetType());
        Assert.Equal(expected.Count(), actual.Count());
    }

    [Fact]
    public void GetExistingTodoTest()
    {
        var testEntity = TestEntityProvider.GetTestTodo();
        var testId = 0L;

        mockService.Setup(s => s.GetTodo(testId)).Returns(testEntity);

        var expected = testEntity;
        var actual = mockService.Object.GetTodo(testId);

        Assert.NotNull(actual);
        if (actual is not null)
            AssertTodoResponsesEqual(expected, actual);
    }

    [Fact]
    public void GetNonExistentTodoTest()
    {
        long testId = 666L;

        mockService.Setup(s => s.GetTodo(testId)).Returns(null as TodoResponse);

        var actual = mockService.Object.GetTodo(testId);

        Assert.Null(actual);
    }

    [Fact]
    public void GetTodoByNameTest()
    {
        var testEntity = TestEntityProvider.GetTestTodo();
        var testName = testEntity.name ?? "";

        mockService.Setup(s => s.GetTodoByName(testName)).Returns(testEntity);

        var expected = testEntity;
        var actual = mockService.Object.GetTodoByName(testName);

        Assert.NotNull(actual);
        if (actual is not null)
            AssertTodoResponsesEqual(expected, actual);
    }

    [Fact]
    public void AddTodoTest()
    {
        Todo testEntity = TestEntityProvider.GetTestTodo();
        TodoRequest testRequest = TestEntityConverter.GetTodoRequest(testEntity);
        long boardId = 0L;

        mockService.Setup(s => s.AddTodo(boardId, testRequest)).Returns(testEntity);

        var expected = testEntity;
        var actual = mockService.Object.AddTodo(boardId, testRequest);

        AssertTodoResponsesEqual(expected, actual);
    }

    [Fact]
    public void UpdateExistingTodoTest()
    {
        Todo testEntity = TestEntityProvider.GetTestTodo();
        Todo updateTestEntity = TestEntityProvider.GetUpdateTestTodo();
        TodoRequest updateTestRequest = TestEntityConverter.GetTodoRequest(updateTestEntity);
        TodoResponse updatedTestResponse = TestEntityConverter.GetTodoResponse(updateTestRequest);
        long testId = 0L;

        mockService.Setup(s => s.UpdateTodo(testId, updateTestRequest)).Returns(updatedTestResponse);

        var expected = updatedTestResponse;
        var actual = mockService.Object.UpdateTodo(testId, updateTestRequest);

        Assert.NotNull(actual);
        if (actual is not null)
            AssertTodoResponsesEqual(expected, actual);
    }

    [Fact]
    public void UpdateNonExistentTodoTest()
    {
        Todo updateTestEntity = TestEntityProvider.GetUpdateTestTodo();
        TodoRequest updateTestRequest = TestEntityConverter.GetTodoRequest(updateTestEntity);
        long testId = 666L;

        mockService.Setup(s => s.UpdateTodo(testId, updateTestRequest)).Returns(null as TodoResponse);

        var actual = mockService.Object.UpdateTodo(testId, updateTestRequest);

        Assert.Null(actual);
    }

    [Fact]
    public void CloneExistingTodoTest()
    {
        Todo testEntity = TestEntityProvider.GetTestTodo();
        Todo testCloneParams = TestEntityProvider.GetTestCloneParams();
        Todo clonedEntity = TestEntityProvider.GetTestTodo();

        clonedEntity.id = testEntity.id;
        clonedEntity.phase = testEntity.phase;
        clonedEntity.boardId = testEntity.boardId;

        mockService.Setup(s => s.CloneTodo(testCloneParams.id, testCloneParams.phase, testCloneParams.boardId ?? defaultBoardId)).Returns(clonedEntity);

        var expected = clonedEntity;
        var actual = mockService.Object.CloneTodo(testCloneParams.id, testCloneParams.phase, testCloneParams.boardId ?? defaultBoardId);

        Assert.NotNull(actual);
        if (actual is not null)
            AssertTodoResponsesEqual(expected, actual);
    }

    [Fact]
    public void CloneNonExistentTodoTest()
    {
        Todo testCloneParams = TestEntityProvider.GetTestCloneParams();

        mockService.Setup(s => s.CloneTodo(testCloneParams.id, testCloneParams.phase, testCloneParams.boardId ?? defaultBoardId)).Returns(null as TodoResponse);

        var actual = mockService.Object.CloneTodo(testCloneParams.id, testCloneParams.phase, testCloneParams.boardId ?? defaultBoardId);

        Assert.Null(actual);
    }

    [Fact]
    public void DeleteExistingTodoTest()
    {
        Todo testEntity = TestEntityProvider.GetTestTodo();
        long testId = 0L;

        mockService.Setup(s => s.RemoveTodo(testId)).Returns(testEntity);

        var expected = testEntity;
        var actual = mockService.Object.RemoveTodo(testId);

        Assert.NotNull(actual);
        if (actual is not null)
            AssertTodosEqual(expected, actual);
    }

    [Fact]
    public void DeleteNonExistentTodoTest()
    {
        long testId = 666L;

        mockService.Setup(s => s.RemoveTodo(testId)).Returns(null as Todo);

        var actual = mockService.Object.RemoveTodo(testId);

        Assert.Null(actual);
    }

    [Fact]
    public void DeleteAllTodosTest()
    {
        var expected = 0;

        mockService.Setup(s => s.RemoveAllTodos()).Returns(expected);

        var actual = mockService.Object.RemoveAllTodos();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeleteAllTodosFromBoardTest()
    {
        var expected = 0;
        var testId = 0L;

        mockService.Setup(s => s.RemoveAllTodosFromBoard(testId)).Returns(expected);

        var actual = mockService.Object.RemoveAllTodosFromBoard(testId);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetTodoPhaseNameTest()
    {
        int testIdx = TodoCommon.TODO_PHASE_MIN;
        string expected = TodoCommon.GetTodoPhaseName(testIdx);

        mockService.Setup(s => s.GetTodoPhaseName(testIdx)).Returns(expected);

        var actual = mockService.Object.GetTodoPhaseName(testIdx);

        Assert.Equal(expected, actual);
    }
}
