using Library.Domain.Entities;
using Library.Domain.Repositories;
using Library.Repository.Context;

namespace Library.Repository;

public class BorrowHistoryRepository : BaseRepository<BorrowHistory>, IBorrowHistoryRepository
{
    public BorrowHistoryRepository(LibraryContext context) : base(context)
    {
    }
}
