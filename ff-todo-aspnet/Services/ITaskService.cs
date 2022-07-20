using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;
using Task = ff_todo_aspnet.Entities.Task;

namespace ff_todo_aspnet.Services
{
    public interface ITaskService
    {
        TaskResponse AddTask(long todoId, TaskRequest taskRequest);
        IEnumerable<TaskResponse> GetAllTasksFromTodo(long todoId);
        TaskResponse? GetTask(long id);
        IEnumerable<TaskResponse> GetTasks();
        long RemoveAllTasks();
        long RemoveAllTasksFromTodo(long todoId);
        Task? RemoveTask(long id);
        TaskResponse? UpdateTask(long id, TaskRequest patchRequest);
    }
}