namespace ff_todo_aspnet.RequestObjects
{
    public class TaskRequest
    {
		public static implicit operator Entities.Task(TaskRequest tr)
		{
			return new Entities.Task
			{
				name = tr.name,
				done = tr.done
			};
		}
		public string name { get; set; }
		public bool done { get; set; }
		public DateTime? deadline { get; set; }
	}
}
