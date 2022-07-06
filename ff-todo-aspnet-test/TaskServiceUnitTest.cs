﻿using ff_todo_aspnet.Constants;
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

    private Task GetTestTask()
    {
        return new Task
        {
            name = "Test task",
            done = false
        };
    }

    private Task GetUpdateTestTask()
    {
        return new Task
        {
            name = "Updated test task",
            done = true
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
    private TaskResponse GetTaskResponse(TaskRequest task)
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
    public void GetTodosFromExistingBoardTest()
    {
        var testTasks = GetTestTaskResponses();
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
        var testTasks = GetTestTaskResponses();

        mockService.Setup(s => s.GetTasks()).Returns(testTasks);

        var expected = testTasks;
        var actual = mockService.Object.GetTasks();

        Assert.Equal(expected.GetType(), actual.GetType());
        Assert.Equal(expected.Count(), actual.Count());
    }

    [Fact]
    public void GetExistingTaskTest()
    {
        var testEntity = GetTestTask();
        var testId = 0L;

        mockService.Setup(s => s.GetTask(testId)).Returns(testEntity);

        var expected = testEntity;
        var actual = mockService.Object.GetTask(testId);

        Assert.NotNull(actual);
        if (actual is not null)
            AssertTaskResponsesEqual(expected, actual);
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
        Task testEntity = GetTestTask();
        TaskRequest testRequest = GetTaskRequest(testEntity);
        long todoId = 0L;

        mockService.Setup(s => s.AddTask(todoId, testRequest)).Returns(testEntity);

        var expected = testEntity;
        var actual = mockService.Object.AddTask(todoId, testRequest);

        AssertTodosEqual(expected, actual);
    }

    [Fact]
    public void UpdateExistingTaskTest()
    {
        Task testEntity = GetTestTask();
        Task updateTestEntity = GetUpdateTestTask();
        TaskRequest updateTestRequest = GetTaskRequest(updateTestEntity);
        TaskResponse updatedTestResponse = GetTaskResponse(updateTestRequest);
        long testId = 0L;

        mockService.Setup(s => s.UpdateTask(testId, updateTestRequest)).Returns(updatedTestResponse);

        var expected = updatedTestResponse;
        var actual = mockService.Object.UpdateTask(testId, updateTestRequest);

        Assert.NotNull(actual);
        if (actual is not null)
            AssertTaskResponsesEqual(expected, actual);
    }

    [Fact]
    public void UpdateNonExistentTodoTest()
    {
        Task updateTestEntity = GetUpdateTestTask();
        TaskRequest updateTestRequest = GetTaskRequest(updateTestEntity);
        long testId = 666L;

        mockService.Setup(s => s.UpdateTask(testId, updateTestRequest)).Returns(null as TaskResponse);

        var actual = mockService.Object.UpdateTask(testId, updateTestRequest);

        Assert.Null(actual);
    }

    [Fact]
    public void DeleteExistingTaskTest()
    {
        Task testEntity = GetTestTask();
        long testId = 0L;

        mockService.Setup(s => s.RemoveTask(testId)).Returns(testEntity);

        var expected = testEntity;
        var actual = mockService.Object.RemoveTask(testId);

        Assert.NotNull(actual);
        if (actual is not null)
            AssertTodosEqual(expected, actual);
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
