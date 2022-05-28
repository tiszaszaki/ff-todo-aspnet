using Microsoft.AspNetCore.Mvc;
using tiszaszaki_asp_webapp_2022.Entities;
using tiszaszaki_asp_webapp_2022.Services;

namespace tiszaszaki_asp_webapp_2022.Controllers
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
        public IEnumerable<Todo> GetTodos()
        {
            return todoService.GetTodos();
        }
        [HttpPut]
        public Todo AddTodo(Todo todo)
        {
            return todoService.AddTodo(testBoardId, todo);
        }
        [HttpDelete("{id}")]
        public void RemoveTodo(int id)
        {
            todoService.RemoveTodo(id);
        }
        [HttpPatch("{id}")]
        public void UpdateTodo(int id, [FromBody]Todo patchedTodo)
        {
            todoService.UpdateTodo(id, patchedTodo);
        }
    }
}
