namespace ff_todo_aspnet.PivotTables
{
    public class PivotEntityEvent
    {
        public PivotEntityEvent(LatestUpdateRecord.LatestUpdateEvent type, DateTime time, long affectedId, string affectedName)
        {
            this.type = type;
            this.time = time;
            this.affectedId = affectedId;
            this.affectedName = affectedName;
        }

        public LatestUpdateRecord.LatestUpdateEvent type { get; set; }
        public DateTime time { get; set; }
        public long affectedId { get; set; }
        public string affectedName { get; set; }
    }
}
