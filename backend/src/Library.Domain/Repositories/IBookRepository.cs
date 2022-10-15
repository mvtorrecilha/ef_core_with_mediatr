using Library.Domain.Entities;

namespace Library.Domain.Repositories;

public interface IBookRepository : IBaseRepository<Book>
{
    Task<IEnumerable<Book>> GetAllBooksNotLentAsync();
    Task<Book> BookBelongToTheCourseCategoryAsync(Guid id, string studentEmail);
}
