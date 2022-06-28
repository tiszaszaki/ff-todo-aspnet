namespace ff_todo_aspnet.PivotTables
{
    public interface IPivotService
    {
        IEnumerable<BoardReadinessResponse> GetBoardReadiness();
    }
}