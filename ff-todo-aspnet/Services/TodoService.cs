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
        public DateTime fetchNewDateTime()
        {
            return DateTime.Now.ToUniversalTime();
        }
        public Todo AddTodo(long boardId, TodoRequest todoRequest)
        {
            Todo todo = todoRequest;
            todo.dateCreated = fetchNewDateTime();
            todo.dateModified = fetchNewDateTime();
            todo.boardId = boardId;
            return todoRepository.AddTodo(todo);
        }

        public void RemoveTodo(long id)
        {
            todoRepository.RemoveTodo(id);
        }

        public void UpdateTodo(long id, TodoRequest patchRequest)
        {
            Todo patchedTodo = patchRequest;
            patchedTodo.dateModified = fetchNewDateTime();
            todoRepository.UpdateTodo(id, patchedTodo);
        }
    }
}
