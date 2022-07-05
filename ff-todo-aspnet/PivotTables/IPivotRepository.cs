namespace ff_todo_aspnet.PivotTables
{
    public interface IPivotRepository
    {
        PivotResponse<ReadinessRecord> FetchBoardReadiness();
        PivotResponse<ReadinessRecord> FetchTodoReadiness();

        PivotResponse<LatestUpdateRecord> FetchBoardLatestUpdate();
        PivotResponse<LatestUpdateRecord> FetchTodoLatestUpdate();
    }
}