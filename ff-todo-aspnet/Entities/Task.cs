using ff_todo_aspnet.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
	}
}
