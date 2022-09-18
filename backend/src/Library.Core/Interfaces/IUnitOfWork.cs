using Library.Core.Interfaces.Repositories;
using System;

namespace Library.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBookRepository Books { get; }
    IStudentRepository Students { get; }
    IBorrowHistoryRepository BorrowHistories { get; }
    int Complete();
}
