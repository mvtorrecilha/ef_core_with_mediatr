using Library.Core.Interfaces.Repositories;
using Library.Core.Models;
using Library.Repository.Context;

namespace Library.Repository;

public class BorrowHistoryRepository : BaseRepository<BorrowHistory>, IBorrowHistoryRepository
{
    public BorrowHistoryRepository(LibraryContext context) : base(context)
    {
    }
}
