using Microsoft.AspNetCore.Mvc;
using ff_todo_aspnet.ResponseObjects;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.Services;
using ff_todo_aspnet.Constants;

namespace ff_todo_aspnet.Controllers
{
    [ApiController]
    [Route(TodoCommon.taskPath)]
    public class TaskController : Controller
    {
        private readonly TaskService taskService;
        public TaskController(TaskService taskService)
        {
            this.taskService = taskService;
        }
        [HttpGet]
        public IEnumerable<TaskResponse> GetTasks()
        {
            return taskService.GetTasks();
        }
        [HttpGet("{id}")]
        public TaskResponse GetTask(long id)
        {
            return taskService.GetTask(id);
        }
        [HttpDelete("{id}")]
        public void RemoveTask(long id)
        {
            taskService.RemoveTask(id);
        }
        [HttpDelete("clear")]
        public void RemoveAllTasks()
        {
            taskService.RemoveAllTasks();
        }
        [HttpPatch("{id}")]
        public void UpdateTask(long id, [FromBody] TaskRequest patchedTask)
        {
            taskService.UpdateTask(id, patchedTask);
        }
        [HttpGet("name-max-length")]
        public long GetNameMaxLength()
        {
            return TodoCommon.MAX_TASK_NAME_LENGTH;
        }
    }
}
