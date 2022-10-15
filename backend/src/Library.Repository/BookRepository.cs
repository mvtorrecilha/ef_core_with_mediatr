using Library.Domain.Entities;
using Library.Domain.Repositories;
using Library.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Repository;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    public BookRepository(LibraryContext context) : base(context)
    {
    }

    public Task<IEnumerable<Book>> GetAllBooksNotLentAsync()
    {
        return GetWhereAsync(b => !b.IsLent);
    }

    public async Task<Book> BookBelongToTheCourseCategoryAsync(Guid id, string studentEmail)
    {
        var bookBelongToTheCourseCategory = (from book in _context.Set<Book>()
                                             join courseCategory in _context.Set<CourseCategory>()
                                                  on book.CategoryId equals courseCategory.CategoryId
                                             join student in _context.Set<Student>()
                                                  on courseCategory.CourseId equals student.CourseId
                                             where student.Email == studentEmail && book.Id == id
                                             select book).FirstOrDefaultAsync<Book>();

        return await bookBelongToTheCourseCategory;
    }
}
