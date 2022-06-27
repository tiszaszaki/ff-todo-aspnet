using ff_todo_aspnet.Constants;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;
using ff_todo_aspnet.Services;
using Moq;
using System.Collections.ObjectModel;

namespace ff_todo_aspnet_test
{
    internal class TodoServiceUnitTest
    {
        private readonly Mock<ITodoService> mockService = new Mock<ITodoService>();

        private Board GetTestBoard()
        {
            return new Board
            {
                name = "Test board",
                description = "Test description",
                author = "Test author"
            };
        }
        private Todo GetTestTodo()
        {
            return new Todo
            {
                name = "Test todo",
                description = "Test description",
                phase = TodoCommon.TODO_PHASE_MIN
            };
        }

        private Collection<TodoResponse> GetTestTodoResponses()
        {
            var todos = new Collection<TodoResponse>();
            todos.Add(GetTestTodo());
            return todos;
        }

        private TodoRequest GetTodoRequest(Todo todo)
        {
            return new TodoRequest
            {
                name = todo.name,
                description = todo.description,
                phase = todo.phase,
                deadline = todo.deadline
            };
        }
        private TodoResponse GetTodoResponse(TodoRequest todo)
        {
            return new TodoResponse
            {
                name = todo.name,
                description = todo.description,
                phase = todo.phase,
                deadline = todo.deadline
            };
        }

        private void AssertTodoResponsesEqual(TodoResponse expected, TodoResponse actual, bool is_strict = false)
        {
            if (is_strict) Assert.Equal(expected.id, actual.id);
            Assert.Equal(expected.name, actual.name);
            Assert.Equal(expected.description, actual.description);
            Assert.Equal(expected.phase, actual.phase);
            if (is_strict)
            {
                Assert.Equal(expected.dateCreated, actual.dateCreated);
                Assert.Equal(expected.dateModified, actual.dateModified);
            }
            Assert.Equal(expected.deadline, actual.deadline);
            Assert.Equal(expected.boardId, actual.boardId);
        }
        private void AssertTodosEqual(Todo expected, Todo actual, bool is_strict = false)
        {
            if (is_strict) Assert.Equal(expected.id, actual.id);
            Assert.Equal(expected.name, actual.name);
            Assert.Equal(expected.description, actual.description);
            Assert.Equal(expected.phase, actual.phase);
            if (is_strict)
            {
                Assert.Equal(expected.dateCreated, actual.dateCreated);
                Assert.Equal(expected.dateModified, actual.dateModified);
            }
            Assert.Equal(expected.deadline, actual.deadline);
            Assert.Equal(expected.boardId, actual.boardId);
        }

        [Fact]
        public void GetTodosTest()
        {
            var testTodos = GetTestTodoResponses();

            mockService.Setup(s => s.GetTodos()).Returns(testTodos);

            var expected = testTodos;
            var actual = mockService.Object.GetTodos();

            Assert.Equal(expected.GetType(), actual.GetType());
            Assert.Equal(expected.Count(), actual.Count());
        }
    }
}
