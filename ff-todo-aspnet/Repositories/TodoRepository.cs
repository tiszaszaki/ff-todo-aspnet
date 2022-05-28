using tiszaszaki_asp_webapp_2022.Configurations;
using tiszaszaki_asp_webapp_2022.Entities;
using tiszaszaki_asp_webapp_2022.ResponseObjects;

namespace tiszaszaki_asp_webapp_2022.Repositories
{
    public class TodoRepository
    {
        private readonly TodoDbContext context;
        public TodoRepository(TodoDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<TodoResponse> FetchTodos()
        {
            return context.Todos.Select(todo => new TodoResponse{
                id = todo.id,
                name = todo.name,
                description = todo.description,
                phase = todo.phase,
                dateCreated = todo.dateCreated,
                dateModified = todo.dateModified,
                deadline = todo.deadline,
                boardId = todo.boardId
            });
        }
        public Todo AddTodo(Todo todo)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
            return todo;
        }
        public void RemoveTodo(long id)
        {
            var todo = context.Todos.Single(todo => todo.id == id);
            context.Todos.Remove(todo);
            context.SaveChanges();
        }
        public void UpdateTodo(long id, Todo patchedTodo)
        {
            var todo = context.Todos.Single(todo => todo.id == id);
            todo.name = patchedTodo.name;
            todo.description = patchedTodo.description;
            todo.phase = patchedTodo.phase;
            context.SaveChanges();
        }
    }
}
