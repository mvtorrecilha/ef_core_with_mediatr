using Library.Core.Models;
using System;
using System.Threading.Tasks;

namespace Library.Core.Interfaces.Repositories;

public interface IBorrowHistoryRepository : IBaseRepository<BorrowHistory>
{
    Task BorrowBookAsync(Guid id, string studentEmail);
}
