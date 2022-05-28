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
