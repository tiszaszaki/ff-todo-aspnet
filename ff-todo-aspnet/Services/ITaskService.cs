using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;

namespace ff_todo_aspnet.Services
{
    public interface ITaskService
    {
        Entities.Task AddTask(long todoId, TaskRequest taskRequest);
        IEnumerable<TaskResponse> GetAllTasksFromTodo(long todoId);
        TaskResponse? GetTask(long id);
        IEnumerable<TaskResponse> GetTasks();
        void RemoveAllTasks();
        void RemoveAllTasksFromTodo(long todoId);
        Entities.Task? RemoveTask(long id);
        TaskResponse? UpdateTask(long id, TaskRequest patchRequest);
    }
}