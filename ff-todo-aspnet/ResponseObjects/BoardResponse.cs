using ff_todo_aspnet.Entities;

namespace ff_todo_aspnet.ResponseObjects
{
	public class BoardResponse
    {
		public static implicit operator BoardResponse(Board br)
		{
			return new BoardResponse
			{
				id = br.id,
				name = br.name,
				description = br.description,
				author = br.author,
				dateCreated = br.dateCreated,
				readonlyTodos = br.readonlyTodos,
				readonlyTasks = br.readonlyTasks
			};
		}

        public long id { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public string author { get; set; }
		public DateTime dateCreated { get; set; }
		public bool readonlyTodos { get; set; }
		public bool readonlyTasks { get; set; }
		public override string ToString()
		{
			return $"{id}, \"{name}\", \"{description}\", \"{author}\", \"{dateCreated}\", {readonlyTodos}, {readonlyTasks}";
		}
	}
}
