using Microsoft.AspNetCore.Mvc;
using tiszaszaki_asp_webapp_2022.Services;

namespace tiszaszaki_asp_webapp_2022.Controllers
{
    [ApiController]
    [Route("todo")]
    public class TodoController : Controller
    {
        private readonly TodoService todoService;
        public TodoController(TodoService todoService)
        {
            this.todoService = todoService;
        }
        [HttpGet]
        public void Test()
        {
            todoService.Test();
        }
    }
}
