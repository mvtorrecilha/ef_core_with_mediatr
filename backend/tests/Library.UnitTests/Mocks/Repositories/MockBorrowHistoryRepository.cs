using Library.Core.Interfaces.Repositories;
using Library.Core.Models;
using Moq;
using System.Threading.Tasks;

namespace Library.UnitTests.Mocks.Repositories
{
    public class MockBorrowHistoryRepository : Mock<IBorrowHistoryRepository>
    {
        public MockBorrowHistoryRepository() : base(MockBehavior.Strict) { }

        public MockBorrowHistoryRepository MockAddAsync()
        {
            Setup(bh => bh.AddAsync(It.IsAny<BorrowHistory>()))
                .Returns(Task.CompletedTask);

            return this;
        }
    }
}
