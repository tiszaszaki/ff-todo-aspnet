using ff_todo_aspnet.Entities;
using ff_todo_aspnet.Repositories;
using Moq;
using System.Collections.ObjectModel;

namespace ff_todo_aspnet_test
{
    public class BoardUnitTest
    {
        private Mock<IBoardRepository> mock = new Mock<IBoardRepository>();

        [Fact]
        public void FetchBoardsTest()
        {
            mock.Setup(r => r.FetchBoardIds()).Returns(new Collection<long>());
        }

        [Fact]
        public void AddBoardTest()
        {
            mock.Setup(r => r.AddBoard(It.IsAny<Board>())).Returns(new Board());
        }
    }
}
