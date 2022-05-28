using ff_todo_aspnet.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tiszaszaki_asp_webapp_2022.Entities
{
	[Table("todo")]
	public class Todo
    {
		public long id { get; set; }
        [Required]
		public string name { get; set; }
        [StringLength(TodoCommon.MAX_TODO_DESCRIPTION_LENGTH)]
		public string description { get; set; }
		public int phase { get; set; }
        [Column("date_created")]
		public DateTime dateCreated { get; set; }
		[Column("date_modified")]
		public DateTime dateModified { get; set; }
		public DateTime? deadline { get; set; }
	}
}
