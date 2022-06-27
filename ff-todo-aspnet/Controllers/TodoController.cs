using Microsoft.AspNetCore.Mvc;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet.ResponseObjects;
using ff_todo_aspnet.Services;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.Constants;
using System.Collections.ObjectModel;
using Task = ff_todo_aspnet.Entities.Task;

namespace ff_todo_aspnet.Controllers
{
    [ApiController]
    [Route(TodoCommon.todoPath)]
    public class TodoController : Controller
    {
        private readonly ITodoService todoService;
        private readonly ITaskService taskService;

        public TodoController(ITodoService todoService, ITaskService taskService)
        {
            this.todoService = todoService;
            this.taskService = taskService;
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
                return Ok(todoResponse);
            else
                return NotFound(ErrorMessages.TODO_NOT_EXIST_MESSAGE(id));
        }
        [HttpGet("name/{name}")]
        public ActionResult GetTodoByName(string name)
        {
            TodoResponse? todoResponse = todoService.GetTodoByName(name);
            if (todoResponse is not null)
                return Ok(todoResponse);
            else
                return NotFound(ErrorMessages.TODO_NOT_EXIST_MESSAGE(name));
        }
        [HttpDelete("{id}")]
        public ActionResult RemoveTodo(long id)
        {
            Todo? todo=todoService.RemoveTodo(id);
            if (todo is not null)
                return Ok();
            else
                return NotFound(ErrorMessages.TODO_NOT_EXIST_MESSAGE(id));
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
                return NotFound(ErrorMessages.TODO_NOT_EXIST_MESSAGE(id));
        }
        [HttpGet("{id}/clone/{phase}/{boardId}")]
        public ActionResult CloneTodo(long id, int phase, long boardId)
        {
            Todo? todo = todoService.CloneTodo(id, phase, boardId);
            if (todo is not null)
                return Ok(todo);
            else
                return NotFound(ErrorMessages.TODO_NOT_EXIST_MESSAGE(id));
        }
        [HttpGet("{id}/tasks")]
        public IEnumerable<TaskResponse> GetAllTasksFromTodo(long id)
        {
            return taskService.GetAllTasksFromTodo(id);
        }
        [HttpPut("{id}/task")]
        public Task AddTask(long id, [FromBody] TaskRequest task)
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
            String result = todoService.GetTodoPhaseName(idx);
            if (result != "")
                return Ok(new TodoPhaseNameResponse { phase = result });
            else
                return NotFound(ErrorMessages.TODO_PHASE_NOT_EXIST(idx));
        }
    }
}
