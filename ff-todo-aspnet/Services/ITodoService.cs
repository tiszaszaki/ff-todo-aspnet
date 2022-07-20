using ff_todo_aspnet.Entities;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;

namespace ff_todo_aspnet.Services
{
    public interface ITodoService
    {
        TodoResponse AddTodo(long boardId, TodoRequest todoRequest);
        TodoResponse? CloneTodo(long id, int phase, long boardId);
        IEnumerable<TodoResponse> GetAllTodosFromBoard(long boardId);
        TodoResponse? GetTodo(long id);
        TodoResponse? GetTodoByName(string name);
        IEnumerable<TodoResponse> GetTodos();
        long RemoveAllTodos();
        long RemoveAllTodosFromBoard(long boardId);
        Todo? RemoveTodo(long id);
        TodoResponse? UpdateTodo(long id, TodoRequest patchRequest);
        string GetTodoPhaseName(int idx);
    }
}