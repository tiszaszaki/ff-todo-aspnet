using ff_todo_aspnet.Repositories;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;
using Task = ff_todo_aspnet.Entities.Task;

namespace ff_todo_aspnet.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository taskRepository;
        private readonly ILogger<TaskService> logger;
        public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
        {
            this.taskRepository = taskRepository;
            this.logger = logger;
        }
        public IEnumerable<TaskResponse> GetTasks()
        {
            IEnumerable<TaskResponse> result = taskRepository.FetchTasks();
            logger.LogInformation("Fetched {0} Task(s)", result.Count());
            return result;
        }
        public IEnumerable<TaskResponse> GetAllTasksFromTodo(long todoId)
        {
            IEnumerable<TaskResponse> result = taskRepository.FetchAllTasksFromTodo(todoId);
            logger.LogInformation("Fetched {0} Task(s) from Todo with ID ({1})", result.ToList().Count, todoId);
            return result;
        }
        public TaskResponse? GetTask(long id)
        {
            TaskResponse? result = taskRepository.FetchTask(id);
            if (result is not null)
                logger.LogInformation("Successfully fetched Task with ID ({0}): {1}", id, result.ToString());
            else
                logger.LogError("Failed to fetch Task with ID ({0})", id);
            return result;
        }
        private DateTime FetchNewDateTime()
        {
            return DateTime.UtcNow;
        }
        public TaskResponse AddTask(long todoId, TaskRequest taskRequest)
        {
            Task task = taskRequest;
            TaskResponse addedTask;
            var now = FetchNewDateTime();
            task.dateCreated = now;
            task.dateModified = now;
            task.todoId = todoId;
            addedTask = taskRepository.AddTask(task);
            logger.LogInformation("Successfully added new Task: {0}", addedTask.ToString());
            return addedTask;
        }
        public Task? RemoveTask(long id)
        {
            Task? task = taskRepository.RemoveTask(id);
            if (task is not null)
                logger.LogInformation("Successfully removed Task with ID ({0})", id);
            else
                logger.LogError("Failed to remove Task with ID ({0})", id);
            return task;
        }
        public long RemoveAllTasks()
        {
            var taskCount = taskRepository.RemoveAllTasks();
            if (taskCount > 0)
                logger.LogInformation("Successfully removed {0} Tasks", taskCount);
            else
                logger.LogWarning("No Tasks were removed");
            return taskCount;
        }
        public long RemoveAllTasksFromTodo(long todoId)
        {
            var taskCount = taskRepository.RemoveAllTasksFromTodo(todoId);
            if (taskCount > 0)
                logger.LogInformation("Successfully removed {0} Tasks from Todo with ID ({1})", taskCount, todoId);
            else
                logger.LogWarning("No Tasks were removed from Todo with ID ({0})", todoId);
            return taskCount;
        }
        public TaskResponse? UpdateTask(long id, TaskRequest patchRequest)
        {
            Task patchedTask = patchRequest;
            TaskResponse? persistedTask;
            patchedTask.dateModified = FetchNewDateTime();
            persistedTask = taskRepository.UpdateTask(id, patchedTask);
            if (persistedTask is not null)
                logger.LogInformation("Successfully updated Task with ID ({0}): {1}", id, persistedTask.ToString());
            else
                logger.LogError("Failed to update Task with ID ({0})", id);
            return persistedTask;
        }
    }
}
