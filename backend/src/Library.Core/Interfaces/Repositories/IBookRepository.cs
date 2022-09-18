using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Core.Interfaces.Repositories;

public interface IBookRepository : IBaseRepository<Book>
{
    Task<IEnumerable<Book>> GetAllBooksNotLentAsync();
    Task<Book> BookBelongToTheCourseCategoryAsync(Guid id, string studentEmail);
}
