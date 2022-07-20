using ff_todo_aspnet.ResponseObjects;
using Task = ff_todo_aspnet.Entities.Task;

namespace ff_todo_aspnet.Repositories
{
    public interface ITaskRepository
    {
        TaskResponse AddTask(Task task);
        IEnumerable<TaskResponse> FetchAllTasksFromTodo(long todoId);
        TaskResponse? FetchTask(long id);
        TaskResponse? FetchTaskByName(string name);
        IEnumerable<TaskResponse> FetchTasks();
        long RemoveAllTasks();
        long RemoveAllTasksFromTodo(long todoId);
        Task? RemoveTask(long id);
        TaskResponse? UpdateTask(long id, Task patchedTask);
    }
}