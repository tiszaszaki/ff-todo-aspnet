using tiszaszaki_asp_webapp_2022.Entities;
using tiszaszaki_asp_webapp_2022.Repositories;
using tiszaszaki_asp_webapp_2022.ResponseObjects;

namespace tiszaszaki_asp_webapp_2022.Services
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
        public Todo AddTodo(long boardId, Todo todo)
        {
            todo.dateCreated = fetchNewDateTime();
            todo.dateModified = todo.dateCreated;
            //todo.boardId = boardId;
            return todoRepository.AddTodo(todo);
        }

        public void RemoveTodo(long id)
        {
            todoRepository.RemoveTodo(id);
        }

        public void UpdateTodo(long id, Todo patchedTodo)
        {
            patchedTodo.dateModified = fetchNewDateTime();
            todoRepository.UpdateTodo(id, patchedTodo);
        }
    }
}
