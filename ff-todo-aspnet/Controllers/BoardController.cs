using Microsoft.AspNetCore.Mvc;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet.ResponseObjects;
using ff_todo_aspnet.Services;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.Constants;

namespace ff_todo_aspnet.Controllers
{
    [ApiController]
    [Route("board")]
    public class BoardController : Controller
    {
        private readonly BoardService boardService;

        public BoardController(BoardService boardService)
        {
            this.boardService = boardService;
        }
        [HttpGet]
        public IEnumerable<BoardResponse> GetBoards()
        {
            return boardService.GetBoards();
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
        [HttpGet("description-max-length")]
        public long getDescriptionMaxLength()
        {
            return TodoCommon.MAX_BOARD_DESCRIPTION_LENGTH;
        }
        [HttpGet("{id}/readonly-todos")]
        public bool getBoardReadonlyTodosSetting(long id)
        {
            return boardService.getBoardReadonlyTodosSetting(id);
        }
        [HttpPatch("{id}/readonly-todos/{isReadonly}")]
        public void setBoardReadonlyTodosSetting(long id, bool isReadonly)
        {
            boardService.setBoardReadonlyTodosSetting(id, isReadonly);
        }
        [HttpGet("{id}/readonly-tasks")]
        public bool getBoardReadonlyTasksSetting(long id)
        {
            return boardService.getBoardReadonlyTasksSetting(id);
        }
        [HttpPatch("{id}/readonly-tasks/{isReadonly}")]
        public void setBoardReadonlyTasksSetting(long id, bool isReadonly)
        {
            boardService.setBoardReadonlyTodosSetting(id, isReadonly);
        }
    }
}
