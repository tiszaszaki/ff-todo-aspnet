namespace ff_todo_aspnet.Constants
{
    public class TodoCommon
    {
		public const int MAX_BOARD_NAME_LENGTH = 64;
		public const int MAX_BOARD_DESCRIPTION_LENGTH = 1024;
		public const int MAX_BOARD_AUTHOR_LENGTH = 128;

		public const int MAX_TODO_NAME_LENGTH = 128;
		public const int MAX_TODO_DESCRIPTION_LENGTH = 1024;

		public const int MAX_TASK_NAME_LENGTH = 32;

		public const int TODO_PHASE_MIN = 0;
		public const int TODO_PHASE_MAX = 2;

		private static IDictionary<int, string> TodoPhaseNames = new Dictionary<int, string>();

		static TodoCommon()
		{
			TodoPhaseNames.Add(0, "Backlog");
			TodoPhaseNames.Add(1, "In progress");
			TodoPhaseNames.Add(2, "Done");
		}

		public static string GetTodoPhaseName(int idx)
        {
			string result = "";

			if ((idx >= TODO_PHASE_MIN) && (idx <= TODO_PHASE_MAX))
				result = TodoPhaseNames[idx];

			return result;
		}

		public const string boardPath = "board";
		public const string todoPath = "todo";
		public const string taskPath = "task";
		public const string pivotPath = "pivot";

		public const string pivotLabel1 = "board-readiness";
		public const string pivotLabel2 = "todo-readiness";

		public const string FIELD_TRUNCATE_STR = "...";

		public const string TODO_CLONE_SUFFIX = " (cloned)";
		public const string TODO_CLONE_SUFFIX_REGEX = @"\(cloned\)$";
	}
}
