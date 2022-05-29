using ff_todo_aspnet.Configurations;
using ff_todo_aspnet.ResponseObjects;

namespace ff_todo_aspnet.Repositories
{
    public class TaskRepository
    {
        private readonly TodoDbContext context;
        public TaskRepository(TodoDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<TaskResponse> FetchTasks()
        {
            return context.Tasks.Select<Entities.Task, TaskResponse>(task => task);
        }
        public IEnumerable<TaskResponse> FetchTasksFromTodo(long todoId)
        {
            return context.Tasks
                .Select<Entities.Task, TaskResponse>(task => task)
                .Where(task => task.todoId == todoId);
        }
        public TaskResponse FetchTask(long id)
        {
            return context.Tasks.Single(task => task.id == id);
        }
        public TaskResponse FetchTaskByName(string name)
        {
            return context.Tasks.Single(task => task.name == name);
        }
        public Entities.Task AddTask(Entities.Task task)
        {
            context.Tasks.Add(task);
            context.SaveChanges();
            return task;
        }
        public void RemoveTask(long id)
        {
            var task = context.Tasks.Single(task => task.id == id);
            context.Tasks.Remove(task);
            context.SaveChanges();
        }
        public void RemoveAllTasks()
        {
            context.Tasks.RemoveRange(context.Tasks);
        }
        public void RemoveAllTasksFromTodo(long todoId)
        {
            foreach (var task in context.Tasks.Where(task => task.todoId == todoId))
                context.Tasks.Remove(task);
        }
        public void UpdateTask(long id, Entities.Task patchedTask)
        {
            var task = context.Tasks.Single(task => task.id == id);
            task.name = patchedTask.name;
            task.done = patchedTask.done;
            task.deadline = patchedTask.deadline;
            context.SaveChanges();
        }
    }
}
