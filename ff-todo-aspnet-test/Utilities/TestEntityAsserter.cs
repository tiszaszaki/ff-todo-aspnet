using ff_todo_aspnet.Entities;
using ff_todo_aspnet.ResponseObjects;
using Task = ff_todo_aspnet.Entities.Task;

namespace ff_todo_aspnet_test.Utilities;

internal class TestEntityAsserter
{
    public static void AssertBoardResponsesEqual(BoardResponse expected, BoardResponse actual, bool is_strict = false)
    {
        if (is_strict) Assert.Equal(expected.id, actual.id);
        Assert.Equal(expected.name, actual.name);
        Assert.Equal(expected.description, actual.description);
        Assert.Equal(expected.author, actual.author);
        if (is_strict) Assert.Equal(expected.dateCreated, actual.dateCreated);
        Assert.Equal(expected.readonlyTodos, actual.readonlyTodos);
        Assert.Equal(expected.readonlyTasks, actual.readonlyTasks);
    }
    public static void AssertBoardsEqual(Board expected, Board actual, bool is_strict = false)
    {
        Assert.Equal(expected.name, actual.name);
        Assert.Equal(expected.description, actual.description);
        Assert.Equal(expected.author, actual.author);
        if (is_strict) Assert.Equal(expected.dateCreated, actual.dateCreated);
        Assert.Equal(expected.readonlyTodos, actual.readonlyTodos);
        Assert.Equal(expected.readonlyTasks, actual.readonlyTasks);
    }

    public static void AssertTodoResponsesEqual(TodoResponse expected, TodoResponse actual, bool is_strict = false)
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
    public static void AssertTodosEqual(Todo expected, Todo actual, bool is_strict = false)
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

    public static void AssertTaskResponsesEqual(TaskResponse expected, TaskResponse actual, bool is_strict = false)
    {
        if (is_strict) Assert.Equal(expected.id, actual.id);
        Assert.Equal(expected.name, actual.name);
        Assert.Equal(expected.done, actual.done);
        Assert.Equal(expected.deadline, actual.deadline);
        Assert.Equal(expected.todoId, actual.todoId);
    }
    public static void AssertTasksEqual(Task expected, Task actual, bool is_strict = false)
    {
        if (is_strict) Assert.Equal(expected.id, actual.id);
        Assert.Equal(expected.name, actual.name);
        Assert.Equal(expected.done, actual.done);
        Assert.Equal(expected.deadline, actual.deadline);
        Assert.Equal(expected.todoId, actual.todoId);
    }
}