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
        public Todo AddTodo(Todo todo)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
            return todo;
        }
        public void RemoveTodo(int id)
        {
            var todo = context.Todos.Single(todo => todo.id == id);
            context.Todos.Remove(todo);
            context.SaveChanges();
        }
        public void UpdateTodo(int id, Todo patchedTodo)
        {
            var todo = context.Todos.Single(todo => todo.id == id);
            todo.name = patchedTodo.name;
            todo.description = patchedTodo.description;
            todo.phase = patchedTodo.phase;
            context.SaveChanges();
        }
    }
}
