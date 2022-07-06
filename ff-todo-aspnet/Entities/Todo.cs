using ff_todo_aspnet.Constants;
using ff_todo_aspnet.PivotTables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ff_todo_aspnet.PivotTables.LatestUpdateRecord;

namespace ff_todo_aspnet.Entities
{
	[Table("todo")]
	public class Todo
    {
        [Key]
		public long id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MaxLength(TodoCommon.MAX_TODO_NAME_LENGTH)]
		public string name { get; set; }
        [MaxLength(TodoCommon.MAX_TODO_DESCRIPTION_LENGTH)]
		public string description { get; set; }
        [Range(TodoCommon.TODO_PHASE_MIN,TodoCommon.TODO_PHASE_MAX)]
		public int phase { get; set; }
        [Column("date_created")]
		public DateTime dateCreated { get; set; }
		[Column("date_modified")]
		public DateTime dateModified { get; set; }
		public DateTime? deadline { get; set; }
		public IEnumerable<Task>? tasks { get; set; }
		[Column("board_id")]
		public long boardId { get; set; }
		public Board board { get; set; }

		public long doneTaskCount()
        {
			if (tasks is not null)
				return tasks.Where(t => t.done).Count();
			else
				return 0;
        }
		public long taskCount()
		{
			if (tasks is not null)
				return tasks.Count();
			else
				return 0;
		}

		private List<PivotEntityEvent> GetEvents()
		{
			var result = new List<PivotEntityEvent> {
				new PivotEntityEvent(LatestUpdateEvent.ADD_TODO, dateCreated, id, name),
				new PivotEntityEvent(LatestUpdateEvent.UPDATE_TODO, dateModified, id, name)
			};
			if (tasks is not null)
				foreach (var t in tasks)
					result.Add(new PivotEntityEvent(t.latestEvent, t.latestUpdated, t.id, t.name));
			return result;
		}

		public DateTime latestUpdated
		{
			get
			{
				return GetEvents().Max(e => e.time);
			}
		}

		public LatestUpdateEvent latestEvent
		{
			get
			{
				return GetEvents().Where(e => e.time == latestUpdated).Select(e => e.type).First();
			}
		}

		public long affectedId
		{
			get
			{
				return GetEvents().Where(e => e.time == latestUpdated).Select(e => e.affectedId).First();
			}
		}

		public string affectedName
		{
			get
			{
				return GetEvents().Where(e => e.time == latestUpdated).Select(e => e.affectedName).First();
			}
		}
	}
}
