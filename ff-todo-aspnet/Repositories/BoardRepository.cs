using ff_todo_aspnet.Configurations;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet.ResponseObjects;
using static ff_todo_aspnet.Configurations.TodoDbContext;

namespace ff_todo_aspnet.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly TodoDbContext context;
        public BoardRepository(TodoDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<BoardResponse> FetchBoards()
        {
            return context.Boards.Select<Board, BoardResponse>(board => board);
        }
        public IEnumerable<long> FetchBoardIds()
        {
            return context.Boards.Select(board => board.id);
        }
        public BoardResponse? FetchBoard(long id)
        {
            if (context.Boards.Count(board => board.id == id) > 0)
                return context.Boards.Single(board => board.id == id);
            else
                return null;
        }
        public BoardResponse AddBoard(Board board)
        {
            board.name = context.ReplaceNameToUnused(TodoDbEntityType.FFTODO_BOARD, board.name ?? "", false);
            context.Boards.Add(board);
            context.SaveChanges();
            return board;
        }
        public Board? RemoveBoard(long id)
        {
            if (context.Boards.Count(board => board.id == id) > 0)
            {
                var board = context.Boards.Single(board => board.id == id);
                context.Boards.Remove(board);
                context.SaveChanges();
                return board;
            }
            else
                return null;
        }
        public BoardResponse? UpdateBoard(long id, Board patchedBoard)
        {
            if (context.Boards.Count(board => board.id == id) > 0)
            {
                var board = context.Boards.Single(board => board.id == id);
                board.name = patchedBoard.name;
                board.description = patchedBoard.description;
                board.author = patchedBoard.author;
                board.dateModified = patchedBoard.dateModified;
                context.SaveChanges();
                return board;
            }
            else
                return null;
        }
        public bool FetchBoardReadonlyTodosSetting(long id)
        {
            return context.Boards.Single(board => board.id == id).readonlyTodos;
        }
        public bool UpdateBoardReadonlyTodosSetting(long id, bool isReadonly)
        {
            var board = context.Boards.Single(board => board.id == id);
            board.readonlyTodos = isReadonly;
            context.SaveChanges();
            return board.readonlyTodos;
        }
        public bool FetchBoardReadonlyTasksSetting(long id)
        {
            return context.Boards.Single(board => board.id == id).readonlyTasks;
        }
        public bool UpdateBoardReadonlyTasksSetting(long id, bool isReadonly)
        {
            var board = context.Boards.Single(board => board.id == id);
            board.readonlyTasks = isReadonly;
            context.SaveChanges();
            return board.readonlyTasks;
        }
    }
}
