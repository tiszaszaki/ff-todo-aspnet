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
        public ActionResult GetBoard(long id)
        {
            BoardResponse? boardResponse = boardService.GetBoard(id);
            if (boardResponse is not null)
                return Ok(boardResponse);
            else
                return NotFound(ErrorMessages.BOARD_NOT_EXIST_MESSAGE(id));
        }
        [HttpPut]
        public Board AddBoard(BoardRequest board)
        {
            return boardService.AddBoard(board);
        }
        [HttpDelete("{id}")]
        public ActionResult RemoveBoard(long id)
        {
            Board? board = boardService.RemoveBoard(id);
            if (board is not null)
                return Ok();
            else
                return NotFound(ErrorMessages.BOARD_NOT_EXIST_MESSAGE(id));
        }
        [HttpPatch("{id}")]
        public ActionResult UpdateBoard(long id, [FromBody] BoardRequest patchedBoard)
        {
            BoardResponse? board = boardService.UpdateBoard(id, patchedBoard);
            if (board is not null)
                return Ok();
            else
                return NotFound(ErrorMessages.BOARD_NOT_EXIST_MESSAGE(id));
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
        [HttpGet("name-max-length")]
        public long GetNameMaxLength()
        {
            return TodoCommon.MAX_BOARD_NAME_LENGTH;
        }
        [HttpGet("description-max-length")]
        public long GetDescriptionMaxLength()
        {
            return TodoCommon.MAX_BOARD_DESCRIPTION_LENGTH;
        }
        [HttpGet("author-max-length")]
        public long GetAuthorMaxLength()
        {
            return TodoCommon.MAX_BOARD_AUTHOR_LENGTH;
        }
        [HttpGet("{id}/readonly-todos")]
        public ActionResult GetBoardReadonlyTodosSetting(long id)
        {
            if (boardService.GetBoard(id) is not null)
                return Ok(boardService.GetBoardReadonlyTodosSetting(id));
            else
                return NotFound(ErrorMessages.BOARD_NOT_EXIST_MESSAGE(id));
        }
        [HttpPatch("{id}/readonly-todos/{isReadonly}")]
        public ActionResult SetBoardReadonlyTodosSetting(long id, bool isReadonly)
        {
            if (boardService.GetBoard(id) is not null)
            {
                boardService.SetBoardReadonlyTodosSetting(id, isReadonly);
                return Ok();
            }
            else
                return NotFound(ErrorMessages.BOARD_NOT_EXIST_MESSAGE(id));
        }
        [HttpGet("{id}/readonly-tasks")]
        public ActionResult GetBoardReadonlyTasksSetting(long id)
        {
            if (boardService.GetBoard(id) is not null)
                return Ok(boardService.GetBoardReadonlyTasksSetting(id));
            else
                return NotFound(ErrorMessages.BOARD_NOT_EXIST_MESSAGE(id));
        }
        [HttpPatch("{id}/readonly-tasks/{isReadonly}")]
        public ActionResult SetBoardReadonlyTasksSetting(long id, bool isReadonly)
        {
            if (boardService.GetBoard(id) is not null)
            {
                boardService.SetBoardReadonlyTodosSetting(id, isReadonly);
                return Ok();
            }
            else
                return NotFound(ErrorMessages.BOARD_NOT_EXIST_MESSAGE(id));
        }
    }
}
