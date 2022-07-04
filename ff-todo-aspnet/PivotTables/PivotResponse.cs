namespace ff_todo_aspnet.PivotTables
{
    public class PivotResponse<T>
    {
        public static ISet<KeyValuePair<string, string>> ExtractFieldsFromType(Type t)
        {
            var res = new HashSet<KeyValuePair<string, string>>();
            var properties = t.GetProperties();
            foreach (var p in properties)
                res.Add(new KeyValuePair<string, string>(p.Name, p.PropertyType.Name));
            return res;
        }
        public ISet<KeyValuePair<string, string>> fields { get; set; }
        public ISet<KeyValuePair<string, string>> fieldDisplay { get; set; }
        public List<string> fieldOrder { get; set; }
        public IEnumerable<T> records { get; set; }
    }
}
