using ff_todo_aspnet.Constants;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;
using System.Collections.ObjectModel;
using Task = ff_todo_aspnet.Entities.Task;

namespace ff_todo_aspnet_test.Utilities;

public class TestEntityProvider
{
    private static readonly string validBoardName = "Test board";
    private static readonly string validBoardDescription = "Test description";
    private static readonly string validBoardAuthor = "Test author";

    private static readonly string validTodoName = "Test todo";
    private static readonly string validTodoDescription = "Test description";
    private static readonly int validTodoPhaseOriginal = TodoCommon.TODO_PHASE_MIN;
    private static readonly int validTodoPhaseUpdated = TodoCommon.TODO_PHASE_MAX;

    private static readonly long clonedTodoId = 0L;
    private static readonly int clonedTodoPhase = TodoCommon.TODO_PHASE_MAX;
    private static readonly long clonedTodoBoardId = 2L;

    private static readonly string validTaskName = "Test task";
    private static readonly bool validTaskDoneOriginal = false;
    private static readonly bool validTaskDoneUpdated = true;

    public static Board GetTestBoard()
    {
        return new Board
        {
            name = validBoardName,
            description = validBoardDescription,
            author = validBoardAuthor
        };
    }
    public static Board GetUpdateTestBoard()
    {
        return new Board
        {
            name = $"{validBoardName} (updated)",
            description = $"{validBoardDescription} (updated)",
            author = $"{validBoardAuthor} (updated)"
        };
    }

    public static Collection<BoardResponse> GetTestBoardResponses()
    {
        var boards = new Collection<BoardResponse>();
        boards.Add(GetTestBoard());
        return boards;
    }
    public static Collection<long> GetTestBoardIds()
    {
        var boardIds = new Collection<long> {
            1L, 2L
        };
        return boardIds;
    }

    public static Collection<BoardRequest> GetInvalidBoardRequests()
    {
        var boards = new Collection<BoardRequest>();
        boards.Add(new BoardRequest // missing name
        {
            description = validBoardDescription,
            author = validBoardAuthor
        });
        boards.Add(new BoardRequest // blank name
        {
            name = "",
            description = validBoardDescription,
            author = validBoardAuthor
        });
        boards.Add(new BoardRequest // name with invalid length
        {
            name = new string('a', TodoCommon.MAX_BOARD_NAME_LENGTH + 1),
            description = validBoardDescription,
            author = validBoardAuthor
        });
        boards.Add(new BoardRequest // description with invalid length
        {
            name = validBoardName,
            description = new string('a', TodoCommon.MAX_BOARD_DESCRIPTION_LENGTH + 1),
            author = validBoardAuthor
        });
        boards.Add(new BoardRequest // author's name with invalid length
        {
            name = validBoardName,
            description = validBoardDescription,
            author = new string('a', TodoCommon.MAX_BOARD_AUTHOR_LENGTH + 1)
        });
        return boards;
    }

    public static Todo GetTestTodo()
    {
        return new Todo
        {
            name = validTodoName,
            description = validTodoDescription,
            phase = validTodoPhaseOriginal
        };
    }
    public static Todo GetTestCloneParams()
    {
        return new Todo
        {
            id = clonedTodoId,
            phase = clonedTodoPhase,
            boardId = clonedTodoBoardId
        };
    }
    public static Todo GetUpdateTestTodo()
    {
        return new Todo
        {
            name = $"{validTodoName} (updated)",
            description = $"{validTodoDescription} (updated)",
            phase = validTodoPhaseUpdated
        };
    }

    public static Collection<TodoResponse> GetTestTodoResponses()
    {
        var todos = new Collection<TodoResponse>();
        todos.Add(GetTestTodo());
        return todos;
    }

    public static Collection<TodoRequest> GetInvalidTodoRequests()
    {
        var boards = new Collection<TodoRequest>();
        boards.Add(new TodoRequest // missing name
        {
            description = validBoardDescription
        });
        boards.Add(new TodoRequest // blank name
        {
            name = "",
            description = validBoardDescription
        });
        boards.Add(new TodoRequest // name with invalid length
        {
            name = new string('a', TodoCommon.MAX_TODO_NAME_LENGTH + 1),
            description = validBoardDescription
        });
        boards.Add(new TodoRequest // description with invalid length
        {
            name = validBoardName,
            description = new string('a', TodoCommon.MAX_TODO_DESCRIPTION_LENGTH + 1)
        });
        return boards;
    }

    public static Task GetTestTask()
    {
        return new Task
        {
            name = validTaskName,
            done = validTaskDoneOriginal
        };
    }
    public static Task GetUpdateTestTask()
    {
        return new Task
        {
            name = $"{validTaskName} (updated)",
            done = validTaskDoneUpdated
        };
    }

    public static Collection<TaskResponse> GetTestTaskResponses()
    {
        var tasks = new Collection<TaskResponse>();
        tasks.Add(GetTestTask());
        return tasks;
    }

    public static Collection<TaskRequest> GetInvalidTaskRequests()
    {
        var boards = new Collection<TaskRequest>();
        boards.Add(new TaskRequest // missing name
        {
            done = validTaskDoneOriginal
        });
        boards.Add(new TaskRequest // blank name
        {
            name = "",
            done = validTaskDoneOriginal
        });
        boards.Add(new TaskRequest // name with invalid length
        {
            name = new string('a', TodoCommon.MAX_TASK_NAME_LENGTH + 1),
            done = validTaskDoneOriginal
        });
        return boards;
    }
}
