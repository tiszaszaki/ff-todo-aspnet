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
            Console.WriteLine("Fetched {0} Task(s) from Todo with ID ({1})", result.ToList().Count, todoId);
            return result;
        }
        public TaskResponse GetTask(long id)
        {
            TaskResponse result = taskRepository.FetchTask(id);
            Console.WriteLine("Successfully fetched Task with ID ({0}): {1}", id, result.ToString());
            return result;
        }
        public Entities.Task AddTask(long todoId, TaskRequest taskRequest)
        {
            Entities.Task task = taskRequest;
            TaskResponse addedTask;
            task.todoId = todoId;
            addedTask = taskRepository.AddTask(task);
            Console.WriteLine("Successfully added new Task: {0}", addedTask.ToString());
            return task;
        }
        public void RemoveTask(long id)
        {
            taskRepository.RemoveTask(id);
            Console.WriteLine("Successfully removed Task with ID {0}", id);
        }
        public void RemoveAllTasks()
        {
            taskRepository.RemoveAllTasks();
            Console.WriteLine("Successfully removed all Tasks");
        }
        public void RemoveAllTasksFromTodo(long todoId)
        {
            taskRepository.RemoveAllTasksFromTodo(todoId);
            Console.WriteLine("Successfully removed all Tasks from Todo with ID ({0})", todoId);
        }
        public void UpdateTask(long id, TaskRequest patchRequest)
        {
            TaskResponse result = taskRepository.UpdateTask(id, patchRequest);
            Console.WriteLine("Successfully updated Task with ID {0}: {1}", id, result.ToString());
        }
    }
}
