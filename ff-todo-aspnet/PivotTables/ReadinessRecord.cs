namespace ff_todo_aspnet.PivotTables
{
    public class ReadinessRecord
    {
        public static double GetPercent(long num, long denom)
        {
            double result = -1;
            if (denom != 0)
                result = (double)num / denom;
            return result;
        }

        [PivotFetch(1, "Key", "ID")]
        public long id { get; set; }
        [PivotFetch(2, "Key", "Name")]
        public string? name { get; set; }
        [PivotFetch(3, "Count of tasks done")]
        public long doneTaskCount { get; set; }
        [PivotFetch(4, "Count of all tasks")]
        public long taskCount { get; set; }
        [PivotFetch(5, "Percent", "% of tasks done")]
        public double doneTaskPercent { get; set; }
    }
}
