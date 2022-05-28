using Microsoft.AspNetCore.Mvc;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet.ResponseObjects;
using ff_todo_aspnet.Services;
using ff_todo_aspnet.RequestObjects;

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
    }
}
