using ff_todo_aspnet.Repositories;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;

namespace ff_todo_aspnet.Services
{
    public class TaskService
    {
        private readonly TaskRepository taskRepository;
        public TaskService(TaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }
        public IEnumerable<TaskResponse> GetTasks()
        {
            return taskRepository.FetchTasks();
        }
        public Entities.Task AddTask(long todoId, TaskRequest taskRequest)
        {
            Entities.Task task = taskRequest;
            task.todoId = todoId;
            return taskRepository.AddTask(task);
        }

        public void RemoveTask(long id)
        {
            taskRepository.RemoveTask(id);
        }

        public void UpdateTask(long id, TaskRequest patchRequest)
        {
            taskRepository.UpdateTask(id, patchRequest);
        }
    }
}
