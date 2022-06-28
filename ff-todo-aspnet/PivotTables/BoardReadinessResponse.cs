namespace ff_todo_aspnet.PivotTables
{
    public class BoardReadinessResponse
    {
        public long id { get; set; }
        public string name { get; set; }
        public double readiness { get; set; }
        public long taskCount { get; set; }
    }
}
