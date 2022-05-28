using tiszaszaki_asp_webapp_2022.Configurations;
using tiszaszaki_asp_webapp_2022.Entities;

namespace tiszaszaki_asp_webapp_2022.Repositories
{
    public class TodoRepository
    {
        private readonly TodoDbContext context;
        public TodoRepository(TodoDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Todo> FetchTodos()
        {
            return context.Todos;
        }
    }
}
