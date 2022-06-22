using ff_todo_aspnet.Entities;
using ff_todo_aspnet.Repositories;
using Moq;

namespace ff_todo_aspnet_test
{
    public class BoardUnitTest
    {
        private Mock<BoardRepository> mock = new Mock<BoardRepository>();

        [Fact]
        public void AddBoardTest()
        {
            mock.Setup(r => r.AddBoard(It.IsAny<Board>())).Returns(new Board());
        }
    }
}
