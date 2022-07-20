using ff_todo_aspnet.Entities;
using ff_todo_aspnet.ResponseObjects;

namespace ff_todo_aspnet.Repositories
{
    public interface ITodoRepository
    {
        string CloneTodoNewName { get; set; }
        string CloneTodoOldName { get; set; }
        bool IsNameTruncated { get; set; }

        TodoResponse AddTodo(Todo todo);
        TodoResponse? CloneTodo(long id, int phase, long boardId, DateTime dateCreatedNew, DateTime dateModifiedNew);
        IEnumerable<TodoResponse> FetchAllTodosFromBoard(long boardId);
        TodoResponse? FetchTodo(long id);
        TodoResponse? FetchTodoByName(string name);
        IEnumerable<TodoResponse> FetchTodos();
        long RemoveAllTodos();
        long RemoveAllTodosFromBoard(long boardId);
        Todo? RemoveTodo(long id);
        TodoResponse? UpdateTodo(long id, Todo patchedTodo);
    }
}