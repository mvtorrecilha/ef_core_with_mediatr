using FluentAssertions;
using Library.UnitTests.Mocks;
using Library.UnitTests.Mocks.Repositories;
using Library.Core.Commands;
using Library.Core.Common;
using Library.Core.Handlers;
using Library.Core.Helpers;
using Library.Core.Notifications;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Library.Core.Models;

namespace Library.UnitTests.Handlers;

public class BorrowBookCommandHandlerTests
{
    protected MockUnitOfWork _mockUnitOfWork;
    protected MockBookRepository _mockBookRepository;
    protected MockStudentRepository _mockStudentRepository;
    protected MockBorrowHistoryRepository _mockBorrowHistoryRepository;
    protected ResponseFormatter _responseFormatterMock;
    protected Notifier _notifier;
    protected MockMediatr _mockMediatr;
    private readonly BorrowBookCommandHandler _handler;

    public BorrowBookCommandHandlerTests()
    {
        _mockUnitOfWork = new MockUnitOfWork();
        _mockBookRepository = new MockBookRepository();
        _mockStudentRepository = new MockStudentRepository();
        _mockBorrowHistoryRepository = new MockBorrowHistoryRepository();
        _notifier = new Notifier();
        _mockMediatr = new MockMediatr();

        _handler = new BorrowBookCommandHandler(
            _mockMediatr.Object,
            _mockUnitOfWork.Object,
            _notifier);
    }

    [Fact]
    public async Task HandleBorrowBookCommand_EmptyEmail_ShouldHasError()
    {
        // Arrange
        var command = new BorrowBookCommand
        {
            BookId = new Guid("762141A7-ACA3-4919-9ACD-3FC86815F05B")
        };

        //Act
        await _handler.Handle(command, default);

        // Assert
        string expectedError = Errors.EmailCannotBeEmpty;
        _notifier.HasError.Should().BeTrue();
        _notifier.Errors.Should().ContainSingle()
            .Which.Message.Should().Be(expectedError);
    }

    [Fact]
    public async Task HandleBorrowBookCommand_StudentNotFoundByEmail_ShouldHasError()
    {
        // Arrange
        var command = new BorrowBookCommand
        {
            BookId = new Guid("762141A7-ACA3-4919-9ACD-3FC86815F05B"),
            StudentEmail = "test@gmail.com"
        };

        _mockStudentRepository.MockGetStudentRegisteredByEmailAsync(command.StudentEmail, null);
        _mockUnitOfWork.MockStudents(_mockStudentRepository);

        //Act
        await _handler.Handle(command, default);

        // Assert
        string expectedError = Errors.StudentNotFound;
        _notifier.HasError.Should().BeTrue();
        _notifier.Errors.Should().ContainSingle()
            .Which.Message.Should().Be(expectedError);
    }

    [Fact]
    public async Task HandleBorrowBookCommand_BookDoesntBelongToStudentCourse_ShouldHasError()
    {
        // Arrange
        var command = new BorrowBookCommand
        {
            BookId = new Guid("762141A7-ACA3-4919-9ACD-3FC86815F05B"),
            StudentEmail = "jr@gmail.com"
        };

        var student = new Student
        {
            Id = Guid.NewGuid(),
            Email = "test@gmail.com"
        };

        _mockStudentRepository.MockGetStudentRegisteredByEmailAsync(command.StudentEmail, student);
        _mockUnitOfWork.MockStudents(_mockStudentRepository);

        _mockBookRepository.MockBookBelongToTheCourseCategoryAsync(command.BookId, command.StudentEmail, null);
        _mockUnitOfWork.MockBooks(_mockBookRepository);
        
        //Act
        await _handler.Handle(command, default);

        // Assert
        string expectedError = Errors.TheBookDoesNotBelongToTheCourseCategory;
        _notifier.HasError.Should().BeTrue();
        _notifier.Errors.Should().ContainSingle()
            .Which.Message.Should().Be(expectedError);
    }

    [Fact]
    public async Task HandleBorrowBookCommand_BookIsLent_ShouldHasError()
    {
        // Arrange
        var command = new BorrowBookCommand
        {
            BookId = new Guid("762141A7-ACA3-4919-9ACD-3FC86815F05B"),
            StudentEmail = "jr@gmail.com"
        };

        var student = new Student
        {
            Id = Guid.NewGuid(),
            Email = "test@gmail.com"
        };

        _mockStudentRepository.MockGetStudentRegisteredByEmailAsync(command.StudentEmail, student);
        _mockUnitOfWork.MockStudents(_mockStudentRepository);

        var lentBook = new Book
        {
            Id = Guid.NewGuid(),
            IsLent = true
        };

        _mockBookRepository.MockBookBelongToTheCourseCategoryAsync(command.BookId, command.StudentEmail, lentBook);
        _mockUnitOfWork.MockBooks(_mockBookRepository);

        //Act
        await _handler.Handle(command, default);

        // Assert
        string expectedError = Errors.BookAlreadyLent;
        _notifier.HasError.Should().BeTrue();
        _notifier.Errors.Should().ContainSingle()
            .Which.Message.Should().Be(expectedError);
    }
}
