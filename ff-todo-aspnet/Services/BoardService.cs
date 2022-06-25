using ff_todo_aspnet.Entities;
using ff_todo_aspnet.Repositories;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;
using System.Collections.ObjectModel;

namespace ff_todo_aspnet.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository boardRepository;
        private readonly ILogger<BoardService> logger;
        public BoardService(IBoardRepository boardRepository, ILogger<BoardService> logger)
        {
            this.boardRepository = boardRepository;
            this.logger = logger;
        }
        public IEnumerable<BoardResponse> GetBoards()
        {
            IEnumerable<BoardResponse> result = boardRepository.FetchBoards();
            logger.LogInformation("Fetched {0} Board(s)", result.Count());
            return result;
        }
        public IEnumerable<long> GetBoardIds()
        {
            IEnumerable<long> result = boardRepository.FetchBoardIds();
            logger.LogInformation("Fetched {0} Board ID(s)", result.Count());
            return result;
        }
        public BoardResponse? GetBoard(long id)
        {
            BoardResponse? result = boardRepository.FetchBoard(id);
            if (result is not null)
                logger.LogInformation("Successfully fetched Board with ID ({0}): {1}", id, result.ToString());
            else
                logger.LogError("Failed to fetch Board with ID ({0})", id);
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
            logger.LogInformation("Successfully added new Board: {0}", addedBoard.ToString());
            return board;
        }
        public Board? RemoveBoard(long id)
        {
            Board? board = boardRepository.RemoveBoard(id);
            if (board is not null)
                logger.LogInformation("Successfully removed Board with ID ({0})", id);
            else
                logger.LogError("Failed to remove Board with ID ({0})", id);
            return board;
        }
        public BoardResponse? UpdateBoard(long id, BoardRequest patchRequest)
        {
            BoardResponse? result = boardRepository.UpdateBoard(id, patchRequest);
            if (result is not null)
                logger.LogInformation("Successfully updated Board with ID ({0}): {1}", id, result.ToString());
            else
                logger.LogError("Failed to update Board with ID ({0})", id);
            return result;
        }
        public bool GetBoardReadonlyTodosSetting(long id)
        {
            bool result = boardRepository.FetchBoardReadonlyTodosSetting(id);
            logger.LogInformation("Successfully queried ReadonlyTodos setting for Board with ID ({0}): {1}", id, result);
            return result;
        }
        public void SetBoardReadonlyTodosSetting(long id, bool isReadonly)
        {
            bool result = boardRepository.UpdateBoardReadonlyTodosSetting(id, isReadonly);
            logger.LogInformation("Successfully changed ReadonlyTodos setting for Board with ID ({0}) to {1}", id, result);
        }
        public bool GetBoardReadonlyTasksSetting(long id)
        {
            bool result = boardRepository.FetchBoardReadonlyTasksSetting(id);
            logger.LogInformation("Successfully queried ReadonlyTasks setting for Board with ID ({0}): {1}", id, result);
            return result;
        }
        public void SetBoardReadonlyTasksSetting(long id, bool isReadonly)
        {
            bool result = boardRepository.UpdateBoardReadonlyTodosSetting(id, isReadonly);
            logger.LogInformation("Successfully changed ReadonlyTasks setting for Board with ID ({0}) to {1}", id, result);
        }
    }
}
