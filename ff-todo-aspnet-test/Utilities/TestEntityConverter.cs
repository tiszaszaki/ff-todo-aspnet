using ff_todo_aspnet.Entities;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;
using Task = ff_todo_aspnet.Entities.Task;

namespace ff_todo_aspnet_test.Utilities;

internal class TestEntityConverter
{
    public static BoardRequest GetBoardRequest(Board board)
    {
        return new BoardRequest
        {
            name = board.name,
            description = board.description,
            author = board.author
        };
    }
    public static BoardResponse GetBoardResponse(BoardRequest board)
    {
        return new BoardResponse
        {
            name = board.name,
            description = board.description,
            author = board.author
        };
    }

    public static TodoRequest GetTodoRequest(Todo todo)
    {
        return new TodoRequest
        {
            name = todo.name,
            description = todo.description,
            phase = todo.phase,
            deadline = todo.deadline
        };
    }
    public static TodoResponse GetTodoResponse(TodoRequest todo)
    {
        return new TodoResponse
        {
            name = todo.name,
            description = todo.description,
            phase = todo.phase,
            deadline = todo.deadline
        };
    }

    public static TaskRequest GetTaskRequest(Task task)
    {
        return new TaskRequest
        {
            name = task.name,
            done = task.done,
            deadline = task.deadline
        };
    }
    public static TaskResponse GetTaskResponse(TaskRequest task)
    {
        return new TaskResponse
        {
            name = task.name,
            done = task.done,
            deadline = task.deadline
        };
    }
}