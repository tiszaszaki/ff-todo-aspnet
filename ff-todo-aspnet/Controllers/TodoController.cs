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
        [HttpGet("{id}")]
        public TodoResponse GetTodo(long id)
        {
            return todoService.GetTodo(id);
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
        [HttpDelete("clear")]
        public void RemoveAllTodos()
        {
            todoService.RemoveAllTodos();
        }
        [HttpDelete("{boardId}/clear")]
        public void RemoveAllTodosFromBoard(long boardId)
        {
            todoService.RemoveAllTodosFromBoard(boardId);
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
        [HttpGet("description-max-length")]
        public long getDescriptionMaxLength()
        {
            return TodoCommon.MAX_BOARD_DESCRIPTION_LENGTH;
        }
        [HttpGet("phase-val-range")]
        public IEnumerable<int> getTodoPhaseRange()
        {
            return new Collection<int>{ TodoCommon.PHASE_MIN, TodoCommon.PHASE_MAX };
        }
    }
}
