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
        public DateTime fetchNewDateTime()
        {
            return DateTime.Now.ToUniversalTime();
        }
        public Board AddBoard(BoardRequest boardRequest)
        {
            Board board = boardRequest;
            board.dateCreated = fetchNewDateTime();
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
        public bool getBoardReadonlyTodosSetting(long id)
        {
            return boardRepository.fetchBoardReadonlyTodosSetting(id);
        }
        public void setBoardReadonlyTodosSetting(long id, bool isReadonly)
        {
            boardRepository.updateBoardReadonlyTodosSetting(id, isReadonly);
        }
        public bool getBoardReadonlyTasksSetting(long id)
        {
            return boardRepository.fetchBoardReadonlyTasksSetting(id);
        }
        public void setBoardReadonlyTasksSetting(long id, bool isReadonly)
        {
            boardRepository.updateBoardReadonlyTodosSetting(id, isReadonly);
        }
    }
}
