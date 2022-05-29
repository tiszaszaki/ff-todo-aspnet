using ff_todo_aspnet.Entities;

namespace ff_todo_aspnet.ResponseObjects
{
	public class TodoResponse
    {
		public static implicit operator TodoResponse(Todo tr)
		{
			return new TodoResponse
			{
				id = tr.id,
				name = tr.name,
				description = tr.description,
				phase = tr.phase,
				dateCreated = tr.dateCreated,
				dateModified = tr.dateModified,
				deadline = tr.deadline,
				boardId = tr.boardId
			};
		}
		public long id { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public int phase { get; set; }
		public DateTime dateCreated { get; set; }
		public DateTime dateModified { get; set; }
		public DateTime? deadline { get; set; }
		public IEnumerable<TaskResponse>? tasks { get; set; }
		public long boardId { get; set; }
	}
}
