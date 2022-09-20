using Library.Core.Interfaces.Repositories;
using Library.Core.Models;
using Moq;
using System;
using System.Collections.Generic;

namespace Library.UnitTests.Mocks.Repositories;

public class MockBookRepository : Mock<IBookRepository>
{
    public MockBookRepository() : base(MockBehavior.Strict) { }

    public MockBookRepository MockGetAllBooksNotLentAsync(List<Book> output)
    {
        Setup(b => b.GetAllBooksNotLentAsync())
            .ReturnsAsync(output);

        return this;
    }

    public MockBookRepository MockGetByIdAsync(Guid id, Book output)
    {
        Setup(b => b.GetByIdAsync(id)).ReturnsAsync(output);

        return this;
    }

    public MockBookRepository MockUpdate(Book input)
    {
        Setup(b => b.Update(input));

        return this;
    }

    public MockBookRepository MockBookBelongToTheCourseCategoryAsync(Guid id, string studentEmail, Book output)
    {
        Setup(b => b.BookBelongToTheCourseCategoryAsync(id, studentEmail)).ReturnsAsync(output);

        return this;
    }

    public MockBookRepository VerifyBookBelongToTheCourseCategoryAsync(Guid id, string studentEmail, Times times)
    {
        Verify(s => s.BookBelongToTheCourseCategoryAsync(id, studentEmail), times);

        return this;
    }

    public MockBookRepository VerifyUpdate(Book input, Times times)
    {
        Verify(s => s.Update(input), times);

        return this;
    }
}
