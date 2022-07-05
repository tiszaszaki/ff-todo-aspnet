using ff_todo_aspnet.Configurations;
using ff_todo_aspnet.ResponseObjects;
using static ff_todo_aspnet.Configurations.TodoDbContext;
using Task = ff_todo_aspnet.Entities.Task;

namespace ff_todo_aspnet.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoDbContext context;
        public TaskRepository(TodoDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<TaskResponse> FetchTasks()
        {
            return context.Tasks.Select<Task, TaskResponse>(task => task);
        }
        public IEnumerable<TaskResponse> FetchAllTasksFromTodo(long todoId)
        {
            return context.Tasks
                .Where(task => task.todoId == todoId)
                .Select<Task, TaskResponse>(task => task);
        }
        public TaskResponse? FetchTask(long id)
        {
            if (context.Tasks.Count(task => task.id == id) > 0)
                return context.Tasks.Single(task => task.id == id);
            else
                return null;
        }
        public TaskResponse? FetchTaskByName(string name)
        {
            if (context.Tasks.Count(task => task.name == name) > 0)
                return context.Tasks.Single(task => task.name == name);
            else
                return null;
        }
        public Task AddTask(Task task)
        {
            task.name = context.ReplaceNameToUnused(TodoDbEntityType.FFTODO_TASK, task.name, false);
            context.Tasks.Add(task);
            context.SaveChanges();
            return task;
        }
        public Task? RemoveTask(long id)
        {
            if (context.Tasks.Count(task => task.id == id) > 0)
            {
                var task = context.Tasks.Single(task => task.id == id);
                context.Tasks.Remove(task);
                context.SaveChanges();
                return task;
            }
            else
                return null;
        }
        public long RemoveAllTasks()
        {
            var taskCount = context.Tasks.Count();
            context.Tasks.RemoveRange(context.Tasks);
            context.SaveChanges();
            return taskCount;
        }
        public long RemoveAllTasksFromTodo(long todoId)
        {
            var taskCount = context.Tasks.Count(task => task.todoId == todoId);
            foreach (var task in context.Tasks.Where(task => task.todoId == todoId))
                context.Tasks.Remove(task);
            context.SaveChanges();
            return taskCount;
        }
        public TaskResponse? UpdateTask(long id, Task patchedTask)
        {
            if (context.Tasks.Count(task => task.id == id) > 0)
            {
                var task = context.Tasks.Single(task => task.id == id);
                task.name = patchedTask.name;
                task.done = patchedTask.done;
                task.dateModified = patchedTask.dateModified;
                task.deadline = patchedTask.deadline;
                context.SaveChanges();
                return task;
            }
            else
                return null;
        }
    }
}
