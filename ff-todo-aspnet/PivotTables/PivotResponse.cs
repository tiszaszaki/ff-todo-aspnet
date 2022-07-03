namespace ff_todo_aspnet.PivotTables
{
    public class PivotResponse<T>
    {
        public static Dictionary<string, string> ExtractFieldsFromType(Type t)
        {
            var res = new Dictionary<string, string>();
            var properties = t.GetProperties();
            foreach (var p in properties)
                res.Add(p.Name, p.PropertyType.Name);
            return res;
        }
        public Dictionary<string, string> fields { get; set; }
        public List<string> fieldOrder { get; set; }
        public IEnumerable<T> records { get; set; }
    }
}
