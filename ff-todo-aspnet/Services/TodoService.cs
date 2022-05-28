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
    }
}
