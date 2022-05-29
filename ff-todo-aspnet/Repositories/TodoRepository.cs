using ff_todo_aspnet.Configurations;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet.ResponseObjects;

namespace ff_todo_aspnet.Repositories
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
            return context.Todos.Select<Todo, TodoResponse>(todo => todo);
        }
        public TodoResponse FetchTodo(long id)
        {
            return context.Todos.Single(todo => todo.id == id);
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
        public void RemoveAllTodos()
        {
            context.Todos.RemoveRange(context.Todos);
        }
        public void RemoveAllTodosFromBoard(long boardId)
        {
            foreach (var todo in context.Todos.Where(todo => todo.boardId == boardId))
                context.Todos.Remove(todo);
        }
        public void UpdateTodo(long id, Todo patchedTodo)
        {
            var todo = context.Todos.Single(todo => todo.id == id);
            todo.name = patchedTodo.name;
            todo.description = patchedTodo.description;
            todo.phase = patchedTodo.phase;
            todo.deadline = patchedTodo.deadline;
            context.SaveChanges();
        }
        public Todo CloneTodo(long id, int phase, long boardId)
        {
            var todo = context.Todos.Single(todo => todo.id == id);
            todo.phase = phase;
            todo.boardId = boardId;
            context.Todos.Add(todo);
            context.SaveChanges();
            return context.Todos.Single(clonedTodo => clonedTodo.name == todo.name);
        }
    }
}
