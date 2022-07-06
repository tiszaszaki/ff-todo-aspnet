namespace ff_todo_aspnet.PivotTables
{
    public class LatestUpdateRecord
    {
        public enum LatestUpdateEvent
        {
            ADD_BOARD,
            ADD_TODO,
            ADD_TASK,
            UPDATE_BOARD,
            UPDATE_TODO,
            UPDATE_TASK
        }

        public static readonly List<string> fieldOrder = new List<string> {
            "id", "name", "latestUpdated", "latestEvent", "affectedId", "affectedName"
        };
        public static readonly IDictionary<string, string> fieldRoles = new Dictionary<string, string> {
            {"id", "Key"}, {"name", "Key"}, {"latestUpdated", ""},
            {"latestEvent", ""}, {"affectedId", ""}, {"affectedName", ""}
        };
        public static readonly IDictionary<string, string> fieldDisplay = new Dictionary<string, string> {
            {"id", "ID"}, {"name", "Name"}, {"latestUpdated", "Date of latest event"},
            {"latestEvent", "Type of latest event"}, {"affectedId", "ID of entity affected"},
            {"affectedName", "Name of entity affected"}
        };

        public long id { get; set; }
        public string name { get; set; }
        public DateTime latestUpdated { get; set; }
        public string latestEvent { get; set; }
        public long affectedId { get; set; }
        public string affectedName { get; set; }
    }
}
