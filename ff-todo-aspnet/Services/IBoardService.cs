using ff_todo_aspnet.Entities;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;

namespace ff_todo_aspnet.Services
{
    public interface IBoardService
    {
        BoardResponse AddBoard(BoardRequest boardRequest);
        BoardResponse? GetBoard(long id);
        IEnumerable<long> GetBoardIds();
        bool GetBoardReadonlyTasksSetting(long id);
        bool GetBoardReadonlyTodosSetting(long id);
        IEnumerable<BoardResponse> GetBoards();
        Board? RemoveBoard(long id);
        void SetBoardReadonlyTasksSetting(long id, bool isReadonly);
        void SetBoardReadonlyTodosSetting(long id, bool isReadonly);
        BoardResponse? UpdateBoard(long id, BoardRequest patchRequest);
    }
}