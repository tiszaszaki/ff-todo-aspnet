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
        public TodoController(TodoService todoService, TaskService taskService)
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
        public TodoResponse GetTodo(long id)
        {
            return todoService.GetTodo(id);
        }
        [HttpGet("name/{name}")]
        public TodoResponse GetTodoByName(string name)
        {
            return todoService.GetTodoByName(name);
        }
        [HttpDelete("{id}")]
        public void RemoveTodo(long id)
        {
            todoService.RemoveTodo(id);
        }
        [HttpDelete("clear")]
        public void RemoveAllTodos()
        {
            todoService.RemoveAllTodos();
        }
        [HttpPatch("{id}")]
        public void UpdateTodo(long id, [FromBody] TodoRequest patchedTodo)
        {
            todoService.UpdateTodo(id, patchedTodo);
        }
        [HttpGet("{id}/clone/{phase}/{boardId}")]
        public Todo CloneTodo(long id, int phase, long boardId)
        {
            return todoService.CloneTodo(id, phase, boardId);
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
            return new Collection<int>{ TodoCommon.PHASE_MIN, TodoCommon.PHASE_MAX };
        }
    }
}
