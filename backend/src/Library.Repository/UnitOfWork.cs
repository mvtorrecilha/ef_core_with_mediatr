using Library.Core.Interfaces;
using Library.Core.Interfaces.Repositories;
using Library.Repository.Context;

namespace Library.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryContext _context;
    public UnitOfWork(LibraryContext context)
    {
        _context = context;
        Books = new BookRepository(_context);
        Students = new StudentRepository(_context);
        BorrowHistories = new BorrowHistoryRepository(_context);
    }

    public IBookRepository Books { get; private set; }
    public IStudentRepository Students { get; private set; }
    public IBorrowHistoryRepository BorrowHistories { get; private set; }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
