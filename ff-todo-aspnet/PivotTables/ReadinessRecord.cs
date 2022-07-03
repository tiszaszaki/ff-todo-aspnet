namespace ff_todo_aspnet.PivotTables
{
    public class ReadinessRecord
    {
        public static readonly List<string> fieldOrder = new List<string> { "id", "name", "doneTaskCount", "taskCount" };
        public long id { get; set; }
        public string name { get; set; }
        public long doneTaskCount { get; set; }
        public long taskCount { get; set; }
    }
}
