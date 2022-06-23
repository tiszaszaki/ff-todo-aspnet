using ff_todo_aspnet.Entities;
using ff_todo_aspnet.ResponseObjects;

namespace ff_todo_aspnet.Repositories
{
    public interface IBoardRepository
    {
        Board AddBoard(Board board);
        BoardResponse? FetchBoard(long id);
        IEnumerable<long> FetchBoardIds();
        bool FetchBoardReadonlyTasksSetting(long id);
        bool FetchBoardReadonlyTodosSetting(long id);
        IEnumerable<BoardResponse> FetchBoards();
        Board? RemoveBoard(long id);
        BoardResponse? UpdateBoard(long id, Board patchedBoard);
        bool UpdateBoardReadonlyTasksSetting(long id, bool isReadonly);
        bool UpdateBoardReadonlyTodosSetting(long id, bool isReadonly);
    }
}