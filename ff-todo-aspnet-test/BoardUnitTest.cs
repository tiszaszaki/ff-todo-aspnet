using ff_todo_aspnet.Controllers;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet.Repositories;
using ff_todo_aspnet.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.ObjectModel;

namespace ff_todo_aspnet_test
{
    public class BoardUnitTest
    {
        private Mock<IBoardRepository> boardMock = new Mock<IBoardRepository>();
        private Mock<ITodoRepository> todoMock = new Mock<ITodoRepository>();
        private ILogger<BoardService> boardLogger = Mock.Of<ILogger<BoardService>>();
        private ILogger<TodoService> todoLogger = Mock.Of<ILogger<TodoService>>();

        private Collection<long> GetTestBoardIds()
        {
            var boardIds = new Collection<long>();
            return boardIds;
        }

        [Fact]
        public void FetchBoardsTest()
        {
            var boardService = new BoardService(boardMock.Object, boardLogger);
            var todoService = new TodoService(todoMock.Object, todoLogger);
            var controller = new BoardController(boardService, todoService);

            boardMock.Setup(r => r.FetchBoardIds()).Returns(GetTestBoardIds());

            var result = controller.GetBoardIds() as OkObjectResult;
            var actual = result as IEnumerable<long>;

            /*
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetTestBoardIds().Count(), actual.Count());
            */
        }

        [Fact]
        public void AddBoardTest()
        {
            boardMock.Setup(r => r.AddBoard(It.IsAny<Board>())).Returns(new Board());
        }
    }
}
