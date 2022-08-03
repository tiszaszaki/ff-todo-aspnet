using ff_todo_aspnet.Constants;
using ff_todo_aspnet.PivotTables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ff_todo_aspnet.PivotTables.LatestUpdateRecord;

namespace ff_todo_aspnet.Entities
{
	[Table("board")]
	public class Board
    {
		[Key]
		public long id { get; set; }
		[Required(AllowEmptyStrings = false)]
		[MaxLength(TodoCommon.MAX_BOARD_NAME_LENGTH)]
		public string? name { get; set; }
		[MaxLength(TodoCommon.MAX_BOARD_DESCRIPTION_LENGTH)]
		public string? description { get; set; }
		[MaxLength(TodoCommon.MAX_BOARD_AUTHOR_LENGTH)]
		public string? author { get; set; }
		[Column("date_created")]
		public DateTime dateCreated { get; set; }
		[Column("date_modified")]
		public DateTime dateModified { get; set; }
		[Column("readonly_todos")]
		public bool readonlyTodos { get; set; }
		[Column("readonly_tasks")]
		public bool readonlyTasks { get; set; }
		public IEnumerable<Todo>? todos { get; set; }

		public long doneTaskCount
		{
			get
			{
				if (todos is not null)
					return todos.Select(t => t.doneTaskCount).Sum();
				else
					return 0;
			}
		}
		public long taskCount
		{
			get
			{
				if (todos is not null)
					return todos.Select(t => t.taskCount).Sum();
				else
					return 0;
			}
		}
		public double doneTaskPercent
        {
			get
            {
				return ReadinessRecord.GetPercent(doneTaskCount, taskCount);
            }
        }

		private List<PivotEntityEvent> GetEvents()
		{
			var result = new List<PivotEntityEvent> {
				new PivotEntityEvent(LatestUpdateEvent.ADD_BOARD, dateCreated, id, name ?? ""),
				new PivotEntityEvent(LatestUpdateEvent.UPDATE_BOARD, dateModified, id, name ?? "")
			};
			if (todos is not null)
				foreach (var t in todos)
					result.Add(new PivotEntityEvent(t.latestEvent, t.latestUpdated, t.affectedId, t.affectedName));
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
