using ff_todo_aspnet.Entities;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.Services;
using Moq;
using System.Collections.ObjectModel;

namespace ff_todo_aspnet_test
{
    public class BoardUnitTest
    {
        private Mock<IBoardService> boardMock = new Mock<IBoardService>();

        private Board GetTestBoard()
        {
            return new Board {
                name = "Test board",
                description = "Test description",
                author = "Test author"
            };
        }
        private Collection<long> GetTestBoardIds()
        {
            var boardIds = new Collection<long> {
                1L, 2L
            };
            return boardIds;
        }

        private void AssertBoardsEqual(Board expected, Board actual, bool is_strict=false)
        {
            Assert.Equal(expected.name, actual.name);
            Assert.Equal(expected.description, actual.description);
            Assert.Equal(expected.author, actual.author);
            if (is_strict) Assert.Equal(expected.dateCreated, actual.dateCreated);
            Assert.Equal(expected.readonlyTodos, actual.readonlyTodos);
            Assert.Equal(expected.readonlyTasks, actual.readonlyTasks);
        }

        [Fact]
        public void GetBoardsTest()
        {
            boardMock.Setup(s => s.GetBoardIds()).Returns(GetTestBoardIds());

            var expected = GetTestBoardIds();
            var actual = boardMock.Object.GetBoardIds();

            Assert.Equal(expected.Count(), actual.Count());
        }

        [Fact]
        public void AddBoardTest()
        {
            Board testEntity = GetTestBoard();
            BoardRequest testRequest = new BoardRequest {
                name = testEntity.name,
                description = testEntity.description,
                author = testEntity.author
            };
            boardMock.Setup(r => r.AddBoard(testRequest)).Returns(testEntity);
            var expected = GetTestBoard();
            var actual = boardMock.Object.AddBoard(testRequest);
            AssertBoardsEqual(expected, actual);
        }
    }
}
