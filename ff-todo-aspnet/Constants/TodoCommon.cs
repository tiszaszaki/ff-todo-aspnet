namespace ff_todo_aspnet.Constants
{
    public class TodoCommon
    {
		public const int MAX_BOARD_DESCRIPTION_LENGTH = 1024;
		public const int MAX_TODO_DESCRIPTION_LENGTH = 1024;
		public const int PHASE_MIN = 0;
		public const int PHASE_MAX = 2;

		public const string boardPath = "board";
		public const string todoPath = "todo";
		public const string taskPath = "task";

		public const string TODO_CLONE_SUFFIX = " (cloned)";
		public const string TODO_CLONE_SUFFIX_REGEX = @"\(cloned\)";
	}
}
