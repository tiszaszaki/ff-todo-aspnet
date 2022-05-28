using ff_todo_aspnet.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ff_todo_aspnet.Entities
{
	[Table("board")]
	public class Board
    {
		[Key]
		public long id { get; set; }
		[Required(AllowEmptyStrings = false)]
		public string name { get; set; }
		[MaxLength(TodoCommon.MAX_BOARD_DESCRIPTION_LENGTH)]
		public string description { get; set; }
		public string author { get; set; }
		[Column("date_created")]
		public DateTime dateCreated { get; set; }
		[Column("readonly_todos")]
		public bool readonlyTodos { get; set; }
		[Column("readonly_tasks")]
		public bool readonlyTasks { get; set; }
		public IEnumerable<Todo>? todos { get; set; }
	}
}
