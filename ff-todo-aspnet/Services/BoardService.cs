using ff_todo_aspnet.Entities;
using ff_todo_aspnet.Repositories;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;
using System.Collections.ObjectModel;

namespace ff_todo_aspnet.Services
{
    public class BoardService
    {
        private readonly BoardRepository boardRepository;
        public BoardService(BoardRepository boardRepository)
        {
            this.boardRepository = boardRepository;
        }
        public IEnumerable<BoardResponse> GetBoards()
        {
            return boardRepository.FetchBoards();
        }
        public BoardResponse GetBoard(long id)
        {
            return boardRepository.FetchBoard(id);
        }
        public DateTime FetchNewDateTime()
        {
            return DateTime.Now.ToUniversalTime();
        }
        public Board AddBoard(BoardRequest boardRequest)
        {
            Board board = boardRequest;
            board.dateCreated = FetchNewDateTime();
            board.todos = new Collection<Todo>();
            return boardRepository.AddBoard(board);
        }
        public void RemoveBoard(long id)
        {
            boardRepository.RemoveBoard(id);
        }
        public void UpdateBoard(long id, BoardRequest patchRequest)
        {
            boardRepository.UpdateBoard(id, patchRequest);
        }
        public bool GetBoardReadonlyTodosSetting(long id)
        {
            return boardRepository.FetchBoardReadonlyTodosSetting(id);
        }
        public void SetBoardReadonlyTodosSetting(long id, bool isReadonly)
        {
            boardRepository.UpdateBoardReadonlyTodosSetting(id, isReadonly);
        }
        public bool GetBoardReadonlyTasksSetting(long id)
        {
            return boardRepository.FetchBoardReadonlyTasksSetting(id);
        }
        public void SetBoardReadonlyTasksSetting(long id, bool isReadonly)
        {
            boardRepository.UpdateBoardReadonlyTodosSetting(id, isReadonly);
        }
    }
}
