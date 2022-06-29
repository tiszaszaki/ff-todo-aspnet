namespace ff_todo_aspnet.PivotTables
{
    public class BoardReadinessResponse
    {
        public long id { get; set; }
        public string name { get; set; }
        public long doneTaskCount { get; set; }
        public long TaskCount { get; set; }
    }
}
