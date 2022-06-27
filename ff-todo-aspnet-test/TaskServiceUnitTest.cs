using ff_todo_aspnet.Constants;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;
using ff_todo_aspnet.Services;
using Moq;
using System.Collections.ObjectModel;
using Task = ff_todo_aspnet.Entities.Task;

namespace ff_todo_aspnet_test;
public class TaskServiceUnitTest
{
    private readonly Mock<ITaskService> mockService = new Mock<ITaskService>();

    private Board GetTestBoard()
    {
        return new Board
        {
            name = "Test board",
            description = "Test description",
            author = "Test author"
        };
    }
    private Todo GetTestTodo()
    {
        return new Todo
        {
            name = "Test todo",
            description = "Test description",
            phase = TodoCommon.TODO_PHASE_MIN
        };
    }
    private Task GetTestTask()
    {
        return new Task
        {
            name = "Test task",
            done = false
        };
    }

    private Collection<TaskResponse> GetTestTaskResponses()
    {
        var tasks = new Collection<TaskResponse>();
        tasks.Add(GetTestTask());
        return tasks;
    }

    private TaskRequest GetTaskRequest(Task task)
    {
        return new TaskRequest
        {
            name = task.name,
            done = task.done,
            deadline = task.deadline
        };
    }
    private TaskResponse GetTodoResponse(TaskRequest task)
    {
        return new TaskResponse
        {
            name = task.name,
            done = task.done,
            deadline = task.deadline
        };
    }

    private void AssertTaskResponsesEqual(TaskResponse expected, TaskResponse actual, bool is_strict = false)
    {
        if (is_strict) Assert.Equal(expected.id, actual.id);
        Assert.Equal(expected.name, actual.name);
        Assert.Equal(expected.done, actual.done);
        Assert.Equal(expected.deadline, actual.deadline);
        Assert.Equal(expected.todoId, actual.todoId);
    }
    private void AssertTodosEqual(Task expected, Task actual, bool is_strict = false)
    {
        if (is_strict) Assert.Equal(expected.id, actual.id);
        Assert.Equal(expected.name, actual.name);
        Assert.Equal(expected.done, actual.done);
        Assert.Equal(expected.deadline, actual.deadline);
        Assert.Equal(expected.todoId, actual.todoId);
    }

    [Fact]
    public void GetTasksTest()
    {
        var testTasks = GetTestTaskResponses();

        mockService.Setup(s => s.GetTasks()).Returns(testTasks);

        var expected = testTasks;
        var actual = mockService.Object.GetTasks();

        Assert.Equal(expected.GetType(), actual.GetType());
        Assert.Equal(expected.Count(), actual.Count());
    }
}
