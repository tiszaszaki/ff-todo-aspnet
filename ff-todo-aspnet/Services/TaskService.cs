using ff_todo_aspnet.Repositories;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;

namespace ff_todo_aspnet.Services
{
    public class TaskService
    {
        private readonly TaskRepository taskRepository;
        private readonly ILogger<TaskService> logger;
        public TaskService(TaskRepository taskRepository, ILogger<TaskService> logger)
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
        public Entities.Task AddTask(long todoId, TaskRequest taskRequest)
        {
            Entities.Task task = taskRequest;
            TaskResponse addedTask;
            task.todoId = todoId;
            addedTask = taskRepository.AddTask(task);
            logger.LogInformation("Successfully added new Task: {0}", addedTask.ToString());
            return task;
        }
        public Entities.Task? RemoveTask(long id)
        {
            Entities.Task? task = taskRepository.RemoveTask(id);
            if (task is not null)
                logger.LogInformation("Successfully removed Task with ID ({0})", id);
            else
                logger.LogError("Failed to remove Task with ID ({0})", id);
            return task;
        }
        public void RemoveAllTasks()
        {
            var taskCount = taskRepository.RemoveAllTasks();
            if (taskCount > 0)
                logger.LogInformation("Successfully removed {0} Tasks", taskCount);
            else
                logger.LogWarning("No Tasks were removed");
        }
        public void RemoveAllTasksFromTodo(long todoId)
        {
            var taskCount = taskRepository.RemoveAllTasksFromTodo(todoId);
            if (taskCount > 0)
                logger.LogInformation("Successfully removed {0} Tasks from Todo with ID ({1})", taskCount, todoId);
            else
                logger.LogWarning("No Tasks were removed from Todo with ID ({0})", todoId);
        }
        public TaskResponse? UpdateTask(long id, TaskRequest patchRequest)
        {
            TaskResponse? result = taskRepository.UpdateTask(id, patchRequest);
            if (result is not null)
                logger.LogInformation("Successfully updated Task with ID ({0}): {1}", id, result.ToString());
            else
                logger.LogError("Failed to update Task with ID ({0})", id);
            return result;
        }
    }
}
