namespace ff_todo_aspnet.PivotTables
{
    public interface IPivotService
    {
        PivotResponse<ReadinessRecord> GetBoardReadiness();
        PivotResponse<ReadinessRecord> GetTodoReadiness();

        PivotResponse<LatestUpdateRecord> GetBoardLatestUpdate();
        PivotResponse<LatestUpdateRecord> GetTodoLatestUpdate();
    }
}