using ff_todo_aspnet.Entities;

namespace ff_todo_aspnet.RequestObjects
{
    public class BoardRequest
    {
		public static implicit operator Board(BoardRequest br)
		{
			return new Board{
				name = br.name,
				description = br.description,
				author = br.author,
				readonlyTodos = false,
				readonlyTasks = false
			};
		}
		public string? name { get; set; }
		public string? description { get; set; }
		public string? author { get; set; }
	}
}
