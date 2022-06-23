using ff_todo_aspnet.ResponseObjects;

namespace ff_todo_aspnet.Repositories
{
    public interface ITaskRepository
    {
        Entities.Task AddTask(Entities.Task task);
        IEnumerable<TaskResponse> FetchAllTasksFromTodo(long todoId);
        TaskResponse? FetchTask(long id);
        TaskResponse? FetchTaskByName(string name);
        IEnumerable<TaskResponse> FetchTasks();
        long RemoveAllTasks();
        long RemoveAllTasksFromTodo(long todoId);
        Entities.Task? RemoveTask(long id);
        TaskResponse? UpdateTask(long id, Entities.Task patchedTask);
    }
}