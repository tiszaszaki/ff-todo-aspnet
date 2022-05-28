using tiszaszaki_asp_webapp_2022.Entities;
using tiszaszaki_asp_webapp_2022.Repositories;

namespace tiszaszaki_asp_webapp_2022.Services
{
    public class TodoService
    {
        private readonly TodoRepository todoRepository;
        public TodoService(TodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }
        public IEnumerable<Todo> GetTodos()
        {
            return todoRepository.FetchTodos();
        }
        public DateTime fetchNewDateTime()
        {
            return DateTime.Now.ToUniversalTime();
        }
        public Todo AddTodo(int boardId, Todo todo)
        {
            todo.dateCreated = fetchNewDateTime();
            todo.dateModified = todo.dateCreated;
            todo.boardId = boardId;
            return todoRepository.AddTodo(todo);
        }

        public void RemoveTodo(int id)
        {
            todoRepository.RemoveTodo(id);
        }

        public void UpdateTodo(int id, Todo patchedTodo)
        {
            patchedTodo.dateModified = fetchNewDateTime();
            todoRepository.UpdateTodo(id, patchedTodo);
        }
    }
}
