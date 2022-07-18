using ff_todo_aspnet.Constants;
using ff_todo_aspnet.Entities;
using Task = ff_todo_aspnet.Entities.Task;

namespace ff_todo_aspnet_test.Utilities;

public class TestEntityProvider
{
    public static Board GetTestBoard()
    {
        return new Board
        {
            name = "Test board",
            description = "Test description",
            author = "Test author"
        };
    }
    public static Board GetUpdateTestBoard()
    {
        return new Board
        {
            name = "Updated test board",
            description = "Updated test description",
            author = "Updated test author"
        };
    }

    public static Todo GetTestTodo()
    {
        return new Todo
        {
            name = "Test todo",
            description = "Test description",
            phase = TodoCommon.TODO_PHASE_MIN
        };
    }
    public static Todo GetTestCloneParams()
    {
        return new Todo
        {
            id = 0L,
            phase = TodoCommon.TODO_PHASE_MAX,
            boardId = 2L
        };
    }
    public static Todo GetUpdateTestTodo()
    {
        return new Todo
        {
            name = "Updated test todo",
            description = "Updated test description",
            phase = TodoCommon.TODO_PHASE_MAX
        };
    }

    public static Task GetTestTask()
    {
        return new Task
        {
            name = "Test task",
            done = false
        };
    }
    public static Task GetUpdateTestTask()
    {
        return new Task
        {
            name = "Updated test task",
            done = true
        };
    }
}
