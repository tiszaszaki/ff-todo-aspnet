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

        [PivotFetch(1, "Key", "ID")]
        public long id { get; set; }
        [PivotFetch(2, "Key", "Name")]
        public string? name { get; set; }
        [PivotFetch(3, "Date of latest event")]
        public DateTime latestUpdated { get; set; }
        [PivotFetch(4, "Type of latest event")]
        public string? latestEvent { get; set; }
        [PivotFetch(5, "ID of entity affected")]
        public long affectedId { get; set; }
        [PivotFetch(6, "Name of entity affected")]
        public string? affectedName { get; set; }
    }
}
