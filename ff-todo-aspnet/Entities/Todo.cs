using ff_todo_aspnet.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Range(TodoCommon.PHASE_MIN,TodoCommon.PHASE_MAX)]
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
	}
}
