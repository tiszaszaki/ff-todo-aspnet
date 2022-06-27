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
        private readonly ITaskService taskService;
        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }
        [HttpGet]
        public IEnumerable<TaskResponse> GetTasks()
        {
            return taskService.GetTasks();
        }
        [HttpGet("{id}")]
        public ActionResult GetTask(long id)
        {
            TaskResponse? taskResponse = taskService.GetTask(id);
            if (taskResponse is not null)
                return Ok(taskResponse);
            else
                return NotFound(ErrorMessages.TASK_NOT_EXIST_MESSAGE(id));
        }
        [HttpDelete("{id}")]
        public ActionResult RemoveTask(long id)
        {
            Entities.Task? task = taskService.RemoveTask(id);
            if (task is not null)
                return Ok();
            else
                return NotFound(ErrorMessages.TASK_NOT_EXIST_MESSAGE(id));
        }
        [HttpDelete("clear")]
        public void RemoveAllTasks()
        {
            taskService.RemoveAllTasks();
        }
        [HttpPatch("{id}")]
        public ActionResult UpdateTask(long id, [FromBody] TaskRequest patchedTask)
        {
            TaskResponse? taskResponse = taskService.UpdateTask(id, patchedTask);
            if (taskResponse is not null)
                return Ok();
            else
                return NotFound(ErrorMessages.TASK_NOT_EXIST_MESSAGE(id));
        }
        [HttpGet("name-max-length")]
        public long GetNameMaxLength()
        {
            return TodoCommon.MAX_TASK_NAME_LENGTH;
        }
    }
}
