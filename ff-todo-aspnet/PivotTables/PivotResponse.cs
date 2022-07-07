namespace ff_todo_aspnet.PivotTables
{
    public class PivotResponse<T>
    {
        public ISet<KeyValuePair<string, string>> fields { get; set; }
        public ISet<KeyValuePair<string, string>> fieldDisplay { get; set; }
        public List<string> fieldOrder { get; set; }
        public IEnumerable<T> records { get; set; }
    }
}
