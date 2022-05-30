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
            IEnumerable<BoardResponse> result = boardRepository.FetchBoards();
            Console.WriteLine("Fetched {0} Board(s)", result.Count());
            return result;
        }
        public IEnumerable<long> GetBoardIds()
        {
            IEnumerable<long> result = boardRepository.FetchBoardIds();
            Console.WriteLine("Fetched {0} Board ID(s)", result.Count());
            return result;
        }
        public BoardResponse GetBoard(long id)
        {
            BoardResponse result = boardRepository.FetchBoard(id);
            Console.WriteLine("Successfully fetched Board with ID ({0}): {1}", id, result.ToString());
            return result;
        }
        private DateTime FetchNewDateTime()
        {
            return DateTime.UtcNow;
        }
        public Board AddBoard(BoardRequest boardRequest)
        {
            Board board = boardRequest;
            BoardResponse addedBoard;
            board.dateCreated = FetchNewDateTime();
            board.todos = new Collection<Todo>();
            addedBoard = boardRepository.AddBoard(board);
            Console.WriteLine("Successfully added new Board: {0}", addedBoard.ToString());
            return board;
        }
        public void RemoveBoard(long id)
        {
            boardRepository.RemoveBoard(id);
            Console.WriteLine("Successfully removed Board with ID {0}", id);
        }
        public void UpdateBoard(long id, BoardRequest patchRequest)
        {
            BoardResponse result = boardRepository.UpdateBoard(id, patchRequest);
            Console.WriteLine("Successfully updated Board with ID {0}: {1}", id, result.ToString());
        }
        public bool GetBoardReadonlyTodosSetting(long id)
        {
            bool result = boardRepository.FetchBoardReadonlyTodosSetting(id);
            Console.WriteLine("Successfully queried ReadonlyTodos setting for Board with ID ({0}): {1}", id, result);
            return result;
        }
        public void SetBoardReadonlyTodosSetting(long id, bool isReadonly)
        {
            bool result = boardRepository.UpdateBoardReadonlyTodosSetting(id, isReadonly);
            Console.WriteLine("Successfully changed ReadonlyTodos setting for Board with ID ({0}) to {1}", id, result);
        }
        public bool GetBoardReadonlyTasksSetting(long id)
        {
            bool result = boardRepository.FetchBoardReadonlyTasksSetting(id);
            Console.WriteLine("Successfully queried ReadonlyTasks setting for Board with ID ({0}): {1}", id, result);
            return result;
        }
        public void SetBoardReadonlyTasksSetting(long id, bool isReadonly)
        {
            bool result = boardRepository.UpdateBoardReadonlyTodosSetting(id, isReadonly);
            Console.WriteLine("Successfully changed ReadonlyTasks setting for Board with ID ({0}) to {1}", id, result);
        }
    }
}
