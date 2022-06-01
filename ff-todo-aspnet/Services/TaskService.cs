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
        public TaskResponse GetTask(long id)
        {
            TaskResponse result = taskRepository.FetchTask(id);
            logger.LogInformation("Successfully fetched Task with ID ({0}): {1}", id, result.ToString());
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
        public void RemoveTask(long id)
        {
            taskRepository.RemoveTask(id);
            logger.LogInformation("Successfully removed Task with ID {0}", id);
        }
        public void RemoveAllTasks()
        {
            taskRepository.RemoveAllTasks();
            logger.LogInformation("Successfully removed all Tasks");
        }
        public void RemoveAllTasksFromTodo(long todoId)
        {
            taskRepository.RemoveAllTasksFromTodo(todoId);
            logger.LogInformation("Successfully removed all Tasks from Todo with ID ({0})", todoId);
        }
        public void UpdateTask(long id, TaskRequest patchRequest)
        {
            TaskResponse result = taskRepository.UpdateTask(id, patchRequest);
            logger.LogInformation("Successfully updated Task with ID {0}: {1}", id, result.ToString());
        }
    }
}
