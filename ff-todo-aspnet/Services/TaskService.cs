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
            IEnumerable<TaskResponse> result = taskRepository.FetchTasks();
            Console.WriteLine("Fetched {0} Task(s)", result.Count());
            return result;
        }
        public IEnumerable<TaskResponse> GetAllTasksFromTodo(long todoId)
        {
            IEnumerable<TaskResponse> result = taskRepository.FetchAllTasksFromTodo(todoId);
            Console.WriteLine("Fetched {0} Task(s) from Todo with ID ({1})", result.Count(), todoId);
            return result;
        }
        public TaskResponse GetTask(long id)
        {
            return taskRepository.FetchTask(id);
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
        public void RemoveAllTasks()
        {
            taskRepository.RemoveAllTasks();
        }
        public void RemoveAllTasksFromTodo(long todoId)
        {
            taskRepository.RemoveAllTasksFromTodo(todoId);
        }
        public void UpdateTask(long id, TaskRequest patchRequest)
        {
            taskRepository.UpdateTask(id, patchRequest);
        }
    }
}
