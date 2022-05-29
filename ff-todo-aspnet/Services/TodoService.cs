using ff_todo_aspnet.Entities;
using ff_todo_aspnet.Repositories;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;

namespace ff_todo_aspnet.Services
{
    public class TodoService
    {
        private readonly TodoRepository todoRepository;
        public TodoService(TodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }
        public IEnumerable<TodoResponse> GetTodos()
        {
            return todoRepository.FetchTodos();
        }
        public TodoResponse GetTodo(long id)
        {
            return todoRepository.FetchTodo(id);
        }
        public DateTime FetchNewDateTime()
        {
            return DateTime.Now.ToUniversalTime();
        }
        public Todo AddTodo(long boardId, TodoRequest todoRequest)
        {
            Todo todo = todoRequest;
            todo.dateCreated = FetchNewDateTime();
            todo.dateModified = FetchNewDateTime();
            todo.boardId = boardId;
            return todoRepository.AddTodo(todo);
        }
        public void RemoveTodo(long id)
        {
            todoRepository.RemoveTodo(id);
        }
        public void RemoveAllTodos()
        {
            todoRepository.RemoveAllTodos();
        }
        public void RemoveAllTodosFromBoard(long boardId)
        {
            todoRepository.RemoveAllTodosFromBoard(boardId);
        }
        public void UpdateTodo(long id, TodoRequest patchRequest)
        {
            Todo patchedTodo = patchRequest;
            patchedTodo.dateModified = FetchNewDateTime();
            todoRepository.UpdateTodo(id, patchedTodo);
        }
        public Todo CloneTodo(long id, int phase, long boardId)
        {
            return todoRepository.CloneTodo(id, phase, boardId, FetchNewDateTime(), FetchNewDateTime());
        }
    }
}
