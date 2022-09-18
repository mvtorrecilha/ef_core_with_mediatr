using Library.Core.Interfaces.Repositories;
using Library.Core.Models;
using Moq;

namespace Library.UnitTests.Mocks.Repositories
{
    public class MockBorrowHistoryRepository : Mock<IBorrowHistoryRepository>
    {
        public MockBorrowHistoryRepository() : base(MockBehavior.Strict) { }

        public MockBorrowHistoryRepository MockAddAsync(BorrowHistory input)
        {
            Setup(bh => bh.AddAsync(input));

            return this;
        }
    }
}
