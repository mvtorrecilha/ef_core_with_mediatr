using FluentAssertions;
using Library.Api.Controllers;
using Library.Api.ViewModels;
using Library.Core.Commands;
using Library.Core.Common;
using Library.Core.Helpers;
using Library.Core.Models;
using Library.UnitTests.Mocks;
using Library.UnitTests.Mocks.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTests;

public class BookControllerTests
{
    protected MockUnitOfWork _mockUnitOfWork;
    protected MockBookRepository _mockBookRepository;
    protected ResponseFormatter _responseFormatterMock;
    protected Notifier _notifier;
    protected MockMediatr _mockMediatr;

    public BookControllerTests()
    {
        _mockBookRepository = new MockBookRepository();
        _mockUnitOfWork = new MockUnitOfWork();
        _notifier = new Notifier();
        _mockMediatr = new MockMediatr();
    }

    private BookController GetController()
    {
        _responseFormatterMock = new ResponseFormatter(_notifier);
        return new BookController(
            _mockUnitOfWork.Object, _responseFormatterMock, _mockMediatr.Object);
    }

    [Fact]
    public async Task PostBorrowBook_BorrowedBook_Successfully()
    {
        // Arrange
        _mockMediatr
            .MockSendCommand<BorrowBookCommand>();

        var request = new BorrowBookCommand
        {
            BookId = new Guid("762141A7-ACA3-4919-9ACD-3FC86815F05B")
        };

        // Act
        var controller = GetController();
        await controller.Post(request);

        // Assert
        _mockMediatr.VerifySend<BorrowBookCommand>(Times.Once());
    }

    [Fact]
    public void PostBorrowBook_BookNotFound_ThrowException()
    {
        // Arrange
        Exception exception = new("Book Not found");
        _mockMediatr
            .MockSendWithException<BorrowBookCommand>(exception);

        var request = new BorrowBookCommand
        {
            BookId = Guid.NewGuid()
        };

        // Act
        var controller = GetController();
        Func<Task> action = async () => await controller.Post(request);

        // Assert
        var expectedException = action.Should().ThrowAsync<Exception>();
        expectedException.WithMessage("Book Not found");    
    }

    [Fact]
    public async Task GetBooks_ReturnAllBooks()
    {
        //Arrange
        var books = new List<Book>
        {
            new Book
            {
                Id = Guid.NewGuid(),
                Author = "Author1",
                Pages = 0,
                Publisher = "",
                Title = "Title1"
            },
            new Book
            {
                Id = Guid.NewGuid(),
                Author = "Author2",
                Pages = 0,
                Publisher = "",
                Title = "Title2",
            }
        };

        _mockBookRepository.MockGetAllBooksNotLentAsync(books);
        _mockUnitOfWork.MockBooks(_mockBookRepository);

        // Act
        var controller = GetController();
        var result = await controller.Get();

        // Assert
        var expectedBooks = new OkObjectResult(books.Select(b => new BookViewModel
        {
            Author = b.Author,
            Id = b.Id,
            Pages = b.Pages,
            Publisher = b.Publisher,
            Title = b.Title
        }));

        result.Should().BeEquivalentTo(expectedBooks);
    }
}
