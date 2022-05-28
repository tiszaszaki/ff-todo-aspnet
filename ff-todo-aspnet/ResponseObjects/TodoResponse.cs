namespace tiszaszaki_asp_webapp_2022.ResponseObjects
{
	public class TodoResponse
    {
		public long id { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public int phase { get; set; }
		public DateTime dateCreated { get; set; }
		public DateTime dateModified { get; set; }
		public DateTime? deadline { get; set; }
		public long boardId { get; set; }
	}
}
