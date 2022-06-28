namespace ff_todo_aspnet.PivotTables
{
    public interface IPivotRepository
    {
        IEnumerable<BoardReadinessResponse> FetchBoardReadiness();
    }
}