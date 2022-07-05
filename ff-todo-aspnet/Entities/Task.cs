using ff_todo_aspnet.Constants;
using ff_todo_aspnet.PivotTables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ff_todo_aspnet.PivotTables.LatestUpdateRecord;

namespace ff_todo_aspnet.Entities
{
	[Table("task")]
	public class Task
	{
		[Key]
		public long id { get; set; }
		[Required(AllowEmptyStrings = false)]
		[MaxLength(TodoCommon.MAX_TASK_NAME_LENGTH)]
		public string name { get; set; }
		public bool done { get; set; }
		[Column("date_created")]
		public DateTime dateCreated { get; set; }
		[Column("date_modified")]
		public DateTime dateModified { get; set; }
		public DateTime? deadline { get; set; }
		[Column("todo_id")]
		public long todoId { get; set; }
		public Todo todo { get; set; }

		private List<PivotEntityEvent> GetEvents()
        {
			return new List<PivotEntityEvent> {
				new PivotEntityEvent(LatestUpdateEvent.ADD_TASK, dateCreated, id, name),
				new PivotEntityEvent(LatestUpdateEvent.UPDATE_TASK, dateModified, id, name)
			};
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
	}
}
