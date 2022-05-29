using Microsoft.AspNetCore.Mvc;
using ff_todo_aspnet.ResponseObjects;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.Services;

namespace ff_todo_aspnet.Controllers
{
    [ApiController]
    [Route("task")]
    public class TaskController : Controller
    {
        private readonly TaskService taskService;

        private readonly int testTodoId = 79;
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
    }
}
