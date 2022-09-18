using Library.Core.Interfaces;
using Library.UnitTests.Mocks.Repositories;
using Moq;

namespace Library.UnitTests.Mocks;

public class MockUnitOfWork : Mock<IUnitOfWork>
{
    public MockUnitOfWork() : base(MockBehavior.Strict) { }

    public MockUnitOfWork MockComplete(int output)
    {
        Setup(s => s.Complete())
            .Returns(output);

        return this;
    }

    public MockUnitOfWork MockBooks(MockBookRepository bookRepository)
    {
        Setup(s => s.Books)
            .Returns(bookRepository.Object);

        return this;
    }

    public MockUnitOfWork MockStudents(MockStudentRepository studentRepository)
    {
        Setup(s => s.Students)
            .Returns(studentRepository.Object);

        return this;
    }

    public MockUnitOfWork MockBorrowHistories(MockBorrowHistoryRepository borrowHistoryRepository)
    {
        Setup(s => s.BorrowHistories)
            .Returns(borrowHistoryRepository.Object);

        return this;
    }
}
