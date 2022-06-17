using Microsoft.AspNetCore.Mvc;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet.ResponseObjects;
using ff_todo_aspnet.Services;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.Constants;
using System.Collections.ObjectModel;

namespace ff_todo_aspnet.Controllers
{
    [ApiController]
    [Route(TodoCommon.todoPath)]
    public class TodoController : Controller
    {
        private readonly TodoService todoService;
        private readonly TaskService taskService;

        private readonly ILogger<TodoService> logger;

        public TodoController(TodoService todoService, TaskService taskService, ILogger<TodoService> logger)
        {
            this.todoService = todoService;
            this.taskService = taskService;
            this.logger = logger;
        }
        [HttpGet]
        public IEnumerable<TodoResponse> GetTodos()
        {
            return todoService.GetTodos();
        }
        [HttpGet("{id}")]
        public ActionResult GetTodo(long id)
        {
            TodoResponse? todoResponse = todoService.GetTodo(id);
            if (todoResponse is not null)
                return Json(todoResponse);
            else
                return BadRequest(ErrorMessages.TODO_NOT_EXIST_MESSAGE(id));
        }
        [HttpGet("name/{name}")]
        public ActionResult GetTodoByName(string name)
        {
            TodoResponse? todoResponse = todoService.GetTodoByName(name);
            if (todoResponse is not null)
                return Json(todoResponse);
            else
                return BadRequest(ErrorMessages.TODO_NOT_EXIST_MESSAGE(name));
        }
        [HttpDelete("{id}")]
        public ActionResult RemoveTodo(long id)
        {
            Todo? todo=todoService.RemoveTodo(id);
            if (todo is not null)
                return Ok();
            else
                return BadRequest(ErrorMessages.TODO_NOT_EXIST_MESSAGE(id));
        }
        [HttpDelete("clear")]
        public void RemoveAllTodos()
        {
            todoService.RemoveAllTodos();
        }
        [HttpPatch("{id}")]
        public ActionResult UpdateTodo(long id, [FromBody] TodoRequest patchedTodo)
        {
            TodoResponse? todo = todoService.UpdateTodo(id, patchedTodo);
            if (todo is not null)
                return Ok();
            else
                return BadRequest(ErrorMessages.TODO_NOT_EXIST_MESSAGE(id));
        }
        [HttpGet("{id}/clone/{phase}/{boardId}")]
        public ActionResult CloneTodo(long id, int phase, long boardId)
        {
            Todo? todo = todoService.CloneTodo(id, phase, boardId);
            if (todo is not null)
                return Json(todo);
            else
                return BadRequest(ErrorMessages.TODO_NOT_EXIST_MESSAGE(id));
        }
        [HttpGet("{id}/tasks")]
        public IEnumerable<TaskResponse> GetAllTasksFromTodo(long id)
        {
            return taskService.GetAllTasksFromTodo(id);
        }
        [HttpPut("{id}/task")]
        public Entities.Task AddTask(long id, [FromBody] TaskRequest task)
        {
            return taskService.AddTask(id, task);
        }
        [HttpDelete("{id}/task/clear")]
        public void RemoveAllTasksFromTodo(long id)
        {
            taskService.RemoveAllTasksFromTodo(id);
        }
        [HttpGet("name-max-length")]
        public long GetNameMaxLength()
        {
            return TodoCommon.MAX_TODO_NAME_LENGTH;
        }
        [HttpGet("description-max-length")]
        public long GetDescriptionMaxLength()
        {
            return TodoCommon.MAX_TODO_DESCRIPTION_LENGTH;
        }
        [HttpGet("phase-val-range")]
        public IEnumerable<int> GetTodoPhaseRange()
        {
            return new Collection<int>{ TodoCommon.TODO_PHASE_MIN, TodoCommon.TODO_PHASE_MAX };
        }
        [HttpGet("phase-name/{idx}")]
        public ActionResult GetTodoPhaseName(int idx)
        {
            string result = TodoCommon.GetTodoPhaseName(idx);
            ActionResult response;
            if (result != "")
            {
                logger.LogInformation("Querying phase name with index ({0}) for all Todos: {1}", idx, result);
                response = Json(result);
            }
            else
            {
                logger.LogError("Queried empty result for phase name with index ({0})", idx);
                response = BadRequest(ErrorMessages.TODO_PHASE_NOT_EXIST);
            }
            return response;
        }
    }
}
