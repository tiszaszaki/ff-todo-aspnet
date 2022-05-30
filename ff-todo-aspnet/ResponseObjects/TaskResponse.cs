namespace ff_todo_aspnet.ResponseObjects
{
	public class TaskResponse
    {
		public static implicit operator TaskResponse(Entities.Task tr)
		{
			return new TaskResponse
			{
				id = tr.id,
				name = tr.name,
				done = tr.done,
				deadline = tr.deadline,
				todoId = tr.todoId
			};
		}
		public long id { get; set; }
		public string name { get; set; }
		public bool done { get; set; }
		public DateTime? deadline { get; set; }
		public long todoId { get; set; }
		public override string ToString()
		{
			return $"{id}, \"{name}\", {done}, \"{deadline}\"";
		}
	}
}
