namespace ff_todo_aspnet.Constants
{
	public class ErrorMessages
	{
		private static string NOT_EXISTING_ID_ADDSTR(object? id)
        {
			string result = "";
			if (id is int)
				result = $"with ID ({id})";
			if (id is string)
				result = $"with name ({id})";
			return result;
        }
		public static string BOARD_NOT_EXIST_MESSAGE(object id)
		{
			string addStr = NOT_EXISTING_ID_ADDSTR(id);
			return $"Board {addStr} does not exist!";
		}
		public static string TODO_NOT_EXIST_MESSAGE(object id)
		{
			string addStr = NOT_EXISTING_ID_ADDSTR(id);
			return $"Todo {addStr} does not exist!";
		}
		public static string TASK_NOT_EXIST_MESSAGE(object id)
		{
			string addStr = NOT_EXISTING_ID_ADDSTR(id);
			return $"Task {addStr} does not exist!";
		}
		public static string TODO_PHASE_NOT_EXIST(int idx)
		{
			return $"Phase index ({idx}) is out of range.";
		}
	}
}
