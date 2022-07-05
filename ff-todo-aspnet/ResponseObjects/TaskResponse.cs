using Task = ff_todo_aspnet.Entities.Task;

namespace ff_todo_aspnet.ResponseObjects
{
	public class TaskResponse
    {
		public static implicit operator TaskResponse(Task tr)
		{
			return new TaskResponse
			{
				id = tr.id,
				name = tr.name,
				done = tr.done,
				dateCreated = tr.dateCreated,
				dateModified = tr.dateModified,
				deadline = tr.deadline,
				todoId = tr.todoId
			};
		}
		public long id { get; set; }
		public string name { get; set; }
		public bool done { get; set; }
		public DateTime dateCreated { get; set; }
		public DateTime dateModified { get; set; }
		public DateTime? deadline { get; set; }
		public long todoId { get; set; }
		public override string ToString()
		{
			return $"[{id}, \"{name}\", {done}, \"{deadline}\"]";
		}
	}
}
