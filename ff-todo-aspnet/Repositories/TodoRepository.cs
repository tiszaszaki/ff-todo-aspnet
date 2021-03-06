using ff_todo_aspnet.Configurations;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet.ResponseObjects;
using static ff_todo_aspnet.Configurations.TodoDbContext;
using Task = ff_todo_aspnet.Entities.Task;

namespace ff_todo_aspnet.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext context;
        public bool IsNameTruncated { get; set; }
        public string CloneTodoOldName { get; set; }
        public string CloneTodoNewName { get; set; }
        public TodoRepository(TodoDbContext context)
        {
            this.context = context;
            IsNameTruncated = false;
            CloneTodoOldName = ""; CloneTodoNewName = "";
        }
        public IEnumerable<TodoResponse> FetchTodos()
        {
            return context.Todos.Select<Todo, TodoResponse>(todo => todo);
        }
        public IEnumerable<TodoResponse> FetchAllTodosFromBoard(long boardId)
        {
            return context.Todos
                .Where(todo => todo.boardId == boardId)
                .Select<Todo, TodoResponse>(todo => todo);
        }
        public TodoResponse? FetchTodo(long id)
        {
            if (context.Todos.Count(todo => todo.id == id) > 0)
                return context.Todos.Single(todo => todo.id == id);
            else
                return null;
        }
        public TodoResponse? FetchTodoByName(string name)
        {
            if (context.Todos.Count(todo => todo.name == name) > 0)
                return context.Todos.Single(todo => todo.name == name);
            else
                return null;
        }
        public TodoResponse AddTodo(Todo todo)
        {
            todo.name = context.ReplaceNameToUnused(TodoDbEntityType.FFTODO_TODO, todo.name ?? "", false);
            context.Todos.Add(todo);
            context.SaveChanges();
            return todo;
        }
        public Todo? RemoveTodo(long id)
        {
            if (context.Todos.Count(todo => todo.id == id) > 0)
            {
                var todo = context.Todos.Single(todo => todo.id == id);
                context.Todos.Remove(todo);
                context.SaveChanges();
                return todo;
            }
            else
                return null;
        }
        public long RemoveAllTodos()
        {
            var todoCount = context.Todos.Count();
            context.Todos.RemoveRange(context.Todos);
            context.SaveChanges();
            return todoCount;
        }
        public long RemoveAllTodosFromBoard(long boardId)
        {
            var todoCount = context.Todos.Count(todo => todo.boardId == boardId);
            foreach (var todo in context.Todos.Where(todo => todo.boardId == boardId))
                context.Todos.Remove(todo);
            context.SaveChanges();
            return todoCount;
        }
        public TodoResponse? UpdateTodo(long id, Todo patchedTodo)
        {
            if (context.Todos.Count(todo => todo.id == id) > 0)
            {
                var todo = context.Todos.Single(todo => todo.id == id);
                todo.name = patchedTodo.name;
                todo.description = patchedTodo.description;
                todo.phase = patchedTodo.phase;
                todo.dateModified = patchedTodo.dateModified;
                todo.deadline = patchedTodo.deadline;
                context.SaveChanges();
                return todo;
            }
            else
                return null;
        }
        private void CloneTasks(long oldTodoId, long newTodoId)
        {
            var tasks = context.Tasks.Where(task => task.todoId == oldTodoId).AsEnumerable();
            foreach (var task in tasks)
            {
                var clonedTask = new Task
                {
                    name = task.name,
                    done = task.done,
                    deadline = task.deadline?.ToUniversalTime(),
                    todoId = newTodoId
                };
                context.Tasks.Add(clonedTask);
            }
            context.SaveChanges();
        }
        public TodoResponse? CloneTodo(long id, int phase, long boardId, DateTime dateCreatedNew, DateTime dateModifiedNew)
        {
            if (context.Todos.Count(todo => todo.id == id) > 0)
            {
                Todo oldTodo = context.Todos.Single(todo => todo.id == id);
                Todo newTodo = new Todo
                {
                    name = context.ReplaceNameToUnused(TodoDbEntityType.FFTODO_TODO, oldTodo.name ?? "", true),
                    description = oldTodo.description,
                    phase = phase,
                    dateCreated = dateCreatedNew,
                    dateModified = dateModifiedNew,
                    deadline = oldTodo.deadline,
                    boardId = boardId
                };
                IsNameTruncated = context.IsNameTruncated;
                CloneTodoOldName = oldTodo.name ?? ""; CloneTodoNewName = newTodo.name;
                context.Todos.Add(newTodo);
                context.SaveChanges();
                CloneTasks(oldTodo.id, newTodo.id);
                return newTodo;
            }
            else
                return null;
        }
    }
}
