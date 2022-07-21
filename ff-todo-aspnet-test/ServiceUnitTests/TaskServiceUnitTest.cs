using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;
using ff_todo_aspnet.Services;
using ff_todo_aspnet_test.Utilities;
using Moq;
using System.Collections.ObjectModel;
using Task = ff_todo_aspnet.Entities.Task;

namespace ff_todo_aspnet_test.ServiceUnitTests;
public class TaskServiceUnitTest
{
    private readonly Mock<ITaskService> mockService = new Mock<ITaskService>();

    [Fact]
    public void GetTodosFromExistingBoardTest()
    {
        var testTasks = TestEntityProvider.GetTestTaskResponses();
        var testId = 0L;

        mockService.Setup(s => s.GetAllTasksFromTodo(testId)).Returns(testTasks);

        var expected = testTasks;
        var actual = mockService.Object.GetAllTasksFromTodo(testId);

        Assert.Equal(expected.GetType(), actual.GetType());
        Assert.Equal(expected.Count(), actual.Count());
    }

    [Fact]
    public void GetTodosFromExistentBoardTest()
    {
        var noTasks = new Collection<TaskResponse>();
        var testId = 666L;

        mockService.Setup(s => s.GetAllTasksFromTodo(testId)).Returns(noTasks);

        var expected = noTasks;
        var actual = mockService.Object.GetAllTasksFromTodo(testId);

        Assert.Equal(expected.GetType(), actual.GetType());
        Assert.Equal(expected.Count(), actual.Count());
    }

    [Fact]
    public void GetTasksTest()
    {
        var testTasks = TestEntityProvider.GetTestTaskResponses();

        mockService.Setup(s => s.GetTasks()).Returns(testTasks);

        var expected = testTasks;
        var actual = mockService.Object.GetTasks();

        Assert.Equal(expected.GetType(), actual.GetType());
        Assert.Equal(expected.Count(), actual.Count());
    }

    [Fact]
    public void GetExistingTaskTest()
    {
        var testEntity = TestEntityProvider.GetTestTask();
        var testId = 0L;

        mockService.Setup(s => s.GetTask(testId)).Returns(testEntity);

        var expected = testEntity;
        var actual = mockService.Object.GetTask(testId);

        Assert.NotNull(actual);
        if (actual is not null)
            TestEntityAsserter.AssertTaskResponsesEqual(expected, actual);
    }

    [Fact]
    public void GetNonExistentTaskTest()
    {
        long testId = 666L;

        mockService.Setup(s => s.GetTask(testId)).Returns(null as TaskResponse);

        var actual = mockService.Object.GetTask(testId);

        Assert.Null(actual);
    }

    [Fact]
    public void AddTaskTest()
    {
        Task testEntity = TestEntityProvider.GetTestTask();
        TaskRequest testRequest = TestEntityConverter.GetTaskRequest(testEntity);
        long todoId = 0L;

        mockService.Setup(s => s.AddTask(todoId, testRequest)).Returns(testEntity);

        var expected = testEntity;
        var actual = mockService.Object.AddTask(todoId, testRequest);

        TestEntityAsserter.AssertTaskResponsesEqual(expected, actual);
    }

    [Fact]
    public void UpdateExistingTaskTest()
    {
        Task testEntity = TestEntityProvider.GetTestTask();
        Task updateTestEntity = TestEntityProvider.GetUpdateTestTask();
        TaskRequest updateTestRequest = TestEntityConverter.GetTaskRequest(updateTestEntity);
        TaskResponse updatedTestResponse = TestEntityConverter.GetTaskResponse(updateTestRequest);
        long testId = 0L;

        mockService.Setup(s => s.UpdateTask(testId, updateTestRequest)).Returns(updatedTestResponse);

        var expected = updatedTestResponse;
        var actual = mockService.Object.UpdateTask(testId, updateTestRequest);

        Assert.NotNull(actual);
        if (actual is not null)
            TestEntityAsserter.AssertTaskResponsesEqual(expected, actual);
    }

    [Fact]
    public void UpdateNonExistentTodoTest()
    {
        Task updateTestEntity = TestEntityProvider.GetUpdateTestTask();
        TaskRequest updateTestRequest = TestEntityConverter.GetTaskRequest(updateTestEntity);
        long testId = 666L;

        mockService.Setup(s => s.UpdateTask(testId, updateTestRequest)).Returns(null as TaskResponse);

        var actual = mockService.Object.UpdateTask(testId, updateTestRequest);

        Assert.Null(actual);
    }

    [Fact]
    public void DeleteExistingTaskTest()
    {
        Task testEntity = TestEntityProvider.GetTestTask();
        long testId = 0L;

        mockService.Setup(s => s.RemoveTask(testId)).Returns(testEntity);

        var expected = testEntity;
        var actual = mockService.Object.RemoveTask(testId);

        Assert.NotNull(actual);
        if (actual is not null)
            TestEntityAsserter.AssertTasksEqual(expected, actual);
    }

    [Fact]
    public void DeleteNonExistentTaskTest()
    {
        long testId = 666L;

        mockService.Setup(s => s.RemoveTask(testId)).Returns(null as Task);

        var actual = mockService.Object.RemoveTask(testId);

        Assert.Null(actual);
    }

    [Fact]
    public void DeleteAllTasksTest()
    {
        var expected = 0;

        mockService.Setup(s => s.RemoveAllTasks()).Returns(expected);

        var actual = mockService.Object.RemoveAllTasks();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeleteAllTasksFromTodoTest()
    {
        var expected = 0;
        var testId = 0L;

        mockService.Setup(s => s.RemoveAllTasksFromTodo(testId)).Returns(expected);

        var actual = mockService.Object.RemoveAllTasksFromTodo(testId);

        Assert.Equal(expected, actual);
    }
}
