namespace ff_todo_aspnet.PivotTables
{
    public class ReadinessRecord
    {
        public static readonly List<string> fieldOrder = new List<string> { "id", "name", "doneTaskCount", "taskCount", "doneTaskPercent" };
        public static readonly IDictionary<string, string> fieldRoles = new Dictionary<string, string> { 
            {"id", "Key"}, {"name", "Key"}, {"doneTaskCount", ""}, {"taskCount", ""}, {"doneTaskPercent", "Percent"}
        };
        public static readonly IDictionary<string, string> fieldDisplay = new Dictionary<string, string> {
            {"id", "ID"}, {"name", "Name"}, {"doneTaskCount", "Count of tasks done"},
            {"taskCount", "Count of all tasks"}, {"doneTaskPercent", "% of tasks done"}
        };

        public static double GetPercent(long num, long denom)
        {
            double result = -1;
            if (denom != 0)
                result = (double)num / denom;
            return result;
        }

        public long id { get; set; }
        public string name { get; set; }
        public long doneTaskCount { get; set; }
        public long taskCount { get; set; }
        public double doneTaskPercent { get; set; }
    }
}
