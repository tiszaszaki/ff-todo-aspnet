using Microsoft.AspNetCore.Mvc;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet.ResponseObjects;
using ff_todo_aspnet.Services;
using ff_todo_aspnet.RequestObjects;

namespace ff_todo_aspnet.Controllers
{
    [ApiController]
    [Route("todo")]
    public class TodoController : Controller
    {
        private readonly TodoService todoService;

        private readonly int testBoardId = 71;
        public TodoController(TodoService todoService)
        {
            this.todoService = todoService;
        }
        [HttpGet]
        public IEnumerable<TodoResponse> GetTodos()
        {
            return todoService.GetTodos();
        }
        [HttpPut]
        public Todo AddTodo(TodoRequest todo)
        {
            return todoService.AddTodo(testBoardId, todo);
        }
        [HttpDelete("{id}")]
        public void RemoveTodo(long id)
        {
            todoService.RemoveTodo(id);
        }
        [HttpPatch("{id}")]
        public void UpdateTodo(long id, [FromBody] TodoRequest patchedTodo)
        {
            todoService.UpdateTodo(id, patchedTodo);
        }
    }
}
