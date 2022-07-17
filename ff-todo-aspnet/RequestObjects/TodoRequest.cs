using ff_todo_aspnet.Entities;

namespace ff_todo_aspnet.RequestObjects
{
    public class TodoRequest
    {
		public static implicit operator Todo(TodoRequest tr)
		{
			return new Todo
			{
				name = tr.name,
				description = tr.description,
				phase = tr.phase,
				deadline = tr.deadline
			};
		}
		public string? name { get; set; }
		public string? description { get; set; }
		public int phase { get; set; }
		public DateTime? deadline { get; set; }
	}
}
