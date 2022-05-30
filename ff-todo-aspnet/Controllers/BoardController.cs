using Microsoft.AspNetCore.Mvc;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet.ResponseObjects;
using ff_todo_aspnet.Services;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.Constants;

namespace ff_todo_aspnet.Controllers
{
    [ApiController]
    [Route(TodoCommon.boardPath)]
    public class BoardController : Controller
    {
        private readonly BoardService boardService;
        private readonly TodoService todoService;
        public BoardController(BoardService boardService, TodoService todoService)
        {
            this.boardService = boardService;
            this.todoService = todoService;
        }
        [HttpGet]
        public IEnumerable<long> GetBoardIds()
        {
            return boardService.GetBoardIds();
        }
        [HttpGet("{id}")]
        public BoardResponse GetBoard(long id)
        {
            return boardService.GetBoard(id);
        }
        [HttpPut]
        public Board AddBoard(BoardRequest board)
        {
            return boardService.AddBoard(board);
        }
        [HttpDelete("{id}")]
        public void RemoveBoard(long id)
        {
            boardService.RemoveBoard(id);
        }
        [HttpPatch("{id}")]
        public void UpdateBoard(long id, [FromBody] BoardRequest patchedBoard)
        {
            boardService.UpdateBoard(id, patchedBoard);
        }
        [HttpGet("{id}/todos")]
        public IEnumerable<TodoResponse> GetAllTodosFromBoard(long id)
        {
            return todoService.GetAllTodosFromBoard(id);
        }
        [HttpPut("{id}/todo")]
        public Todo AddTodo(long id, [FromBody] TodoRequest todo)
        {
            return todoService.AddTodo(id, todo);
        }
        [HttpDelete("{id}/todo/clear")]
        public void RemoveAllTodosFromBoard(long id)
        {
            todoService.RemoveAllTodosFromBoard(id);
        }
        [HttpGet("description-max-length")]
        public long GetDescriptionMaxLength()
        {
            return TodoCommon.MAX_BOARD_DESCRIPTION_LENGTH;
        }
        [HttpGet("{id}/readonly-todos")]
        public bool GetBoardReadonlyTodosSetting(long id)
        {
            return boardService.GetBoardReadonlyTodosSetting(id);
        }
        [HttpPatch("{id}/readonly-todos/{isReadonly}")]
        public void SetBoardReadonlyTodosSetting(long id, bool isReadonly)
        {
            boardService.SetBoardReadonlyTodosSetting(id, isReadonly);
        }
        [HttpGet("{id}/readonly-tasks")]
        public bool GetBoardReadonlyTasksSetting(long id)
        {
            return boardService.GetBoardReadonlyTasksSetting(id);
        }
        [HttpPatch("{id}/readonly-tasks/{isReadonly}")]
        public void SetBoardReadonlyTasksSetting(long id, bool isReadonly)
        {
            boardService.SetBoardReadonlyTodosSetting(id, isReadonly);
        }
    }
}
