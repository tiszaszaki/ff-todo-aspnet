using ff_todo_aspnet.Constants;
using ff_todo_aspnet.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tiszaszaki_asp_webapp_2022.Entities
{
	[Table("todo")]
	public class Todo
    {
        [Key]
		public long id { get; set; }
        [Required(AllowEmptyStrings = false)]
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
		[Column("board_id")]
		public long boardId { get; set; }
		public Board board { get; set; }
	}
}
