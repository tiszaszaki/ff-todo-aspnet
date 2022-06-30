namespace ff_todo_aspnet.PivotTables
{
    public class PivotResponse<T>
    {
        public PivotResponse(IEnumerable<T> records)
        {
            this.records = records;
            fields = ExtractFieldsFromType();
        }
        private Dictionary<string, string> ExtractFieldsFromType()
        {
            var res = new Dictionary<string, string>();
            var fields = typeof(T).GetFields();
            foreach (var f in fields)
                res.Add(f.Name, f.FieldType.Name);
            return res;
        }
        public Dictionary<string, string> fields { get; set; }
        public IEnumerable<T> records { get; set; }
    }
}
