using Task = ff_todo_aspnet.Entities.Task;

namespace ff_todo_aspnet.RequestObjects
{
    public class TaskRequest
    {
		public static implicit operator Task(TaskRequest tr)
		{
			return new Task
			{
				name = tr.name,
				done = tr.done
			};
		}
		public string? name { get; set; }
		public bool done { get; set; }
		public DateTime? deadline { get; set; }
	}
}
