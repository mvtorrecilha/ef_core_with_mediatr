using Library.Domain.Repositories;

namespace Library.Domain;

public interface IUnitOfWork : IDisposable
{
    IBookRepository Books { get; }
    IStudentRepository Students { get; }
    IBorrowHistoryRepository BorrowHistories { get; }
    int Complete();
}
