using Library.Core.Interfaces.Repositories;
using Library.Core.Models;
using Library.Repository.Context;
using System;
using System.Threading.Tasks;

namespace Library.Repository;

public class BorrowHistoryRepository : BaseRepository<BorrowHistory>, IBorrowHistoryRepository
{
    public BorrowHistoryRepository(LibraryContext context) : base(context)
    {
    }

    public Task BorrowBookAsync(Guid id, string studentEmail)
    {
        throw new NotImplementedException();
    }
}
