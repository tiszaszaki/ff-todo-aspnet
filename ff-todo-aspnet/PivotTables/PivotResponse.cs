namespace ff_todo_aspnet.PivotTables
{
    public class PivotResponse<T>
    {

        public static ISet<KeyValuePair<string, string>> ExtractFieldsFromType(Type t)
        {
            var res = new HashSet<KeyValuePair<string, string>>();
            var properties = t.GetProperties();
            foreach (var p in properties)
            {
                var propName = p.Name;
                var propType = p.PropertyType.Name;
                if (Attribute.IsDefined(p, typeof(PivotFetch)))
                {
                    var propAttrs = (PivotFetch[])p.PropertyType.GetCustomAttributes(typeof(PivotFetch), true);
                    if (propAttrs.Length > 0)
                    {
                        var propRole = propAttrs[0].role;
                        if (propRole != "")
                            propType += $",{propRole}";
                    }
                }
                res.Add(new KeyValuePair<string, string>(propName, propType));
            }
            return res;
        }
        public static List<string> ExtractFieldOrderFromType(Type t)
        {
            var propertiesWithOrder = new List<KeyValuePair<string, int>>();
            foreach (var p in t.GetProperties())
            {
                var propName = p.Name;
                var propOrder = 0;
                if (Attribute.IsDefined(p, typeof(PivotFetch)))
                {
                    var propAttrs = (PivotFetch[])p.PropertyType.GetCustomAttributes(typeof(PivotFetch), true);
                    if (propAttrs.Length > 0)
                    {
                        propOrder = propAttrs[0].order;
                        propertiesWithOrder.Add(new KeyValuePair<string, int>(propName, propOrder));
                    }
                }
            }
            return propertiesWithOrder.OrderBy(p => p.Value).Select(p => p.Key).ToList();
        }
        public static ISet<KeyValuePair<string, string>> ExtractFieldDisplayFromType(Type t)
        {
            var res = new HashSet<KeyValuePair<string, string>>();
            var properties = t.GetProperties();
            foreach (var p in properties)
            {
                if (Attribute.IsDefined(p, typeof(PivotFetch)))
                {
                    var propName = p.Name;
                    var propAttrs = (PivotFetch[])p.PropertyType.GetCustomAttributes(typeof(PivotFetch), true);
                    if (propAttrs.Length > 0)
                    {
                        var propDisplay = propAttrs[0].display;
                        if (propDisplay != "")
                            res.Add(new KeyValuePair<string, string>(propName, propDisplay));
                    }
                }
            }
            return res;
        }

        public ISet<KeyValuePair<string, string>> fields { get; set; }
        public ISet<KeyValuePair<string, string>> fieldDisplay { get; set; }
        public List<string> fieldOrder { get; set; }
        public IEnumerable<T> records { get; set; }
    }
}
