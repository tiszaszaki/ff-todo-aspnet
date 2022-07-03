namespace ff_todo_aspnet.PivotTables
{
    public class ReadinessRecord
    {
        public static readonly List<string> fieldOrder = new List<string> { "id", "name", "doneTaskCount", "taskCount", "doneTaskPercent" };
        public static readonly Dictionary<string, string> fieldRoles = new Dictionary<string, string> { 
            {"id", "Key"}, {"name", "Key"}, {"doneTaskCount", ""}, {"taskCount", ""}, {"doneTaskPercent", "Percent"}
        };
        public long id { get; set; }
        public string name { get; set; }
        public long doneTaskCount { get; set; }
        public long taskCount { get; set; }
        public double doneTaskPercent { get; set; }
    }
}
