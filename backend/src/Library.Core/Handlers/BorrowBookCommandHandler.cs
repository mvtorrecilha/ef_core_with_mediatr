using Library.Core.Commands;
using Library.Core.Common;
using Library.Core.Interfaces;
using Library.Core.Models;
using Library.Core.Notifications;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Core.Handlers;

public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand>
{
    private readonly IMediator _mediator;
    private readonly INotifier _notifier;
    private readonly IUnitOfWork _unitOfWork;
    private Guid studentId { get; set; }

    public BorrowBookCommandHandler(
        IMediator mediator,
        IUnitOfWork unitOfWork,
        INotifier notifier)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _notifier = notifier;
    }

    public async Task<Unit> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
        if (!await IsValidToBorrowABook(request))
        {
            return Unit.Value;
        }

        if(await BorrowABook(request.BookId, studentId) <= 0)
        {
            return Unit.Value;
        }

        await _mediator.Publish(new BorrowedBookNotification
        {
            BookId = request.BookId,
            StudentEmail = request.StudentEmail
        }, cancellationToken);

        return Unit.Value;
    }

    private async Task<int> BorrowABook(Guid bookId, Guid studentId)
    {
        var bookToBorrow = new BorrowHistory
        {
            StudentId = studentId,
            BookId = bookId,
           // BorrowDate = DateTime.Now
        };

        var book =  await _unitOfWork.Books.GetByIdAsync(bookId);
        book.IsLent = true;
        _unitOfWork.Books.Update(book);
        await _unitOfWork.BorrowHistories.AddAsync(bookToBorrow);
        return _unitOfWork.Complete();
    }

    private async Task<bool> IsValidToBorrowABook(BorrowBookCommand request)
    {  
        if (String.IsNullOrWhiteSpace(request.StudentEmail))
        {
            _notifier.AddError("Email", Errors.EmailCannotBeEmpty, null);
            _notifier.SetStatuCode(HttpStatusCode.BadRequest);
            return false;
        }

        var studentRegistered = await _unitOfWork.Students.GetStudentRegisteredByEmailAsync(request.StudentEmail);
        if (studentRegistered is null)
        {
            _notifier.AddError("Email", Errors.StudentNotFound, request.StudentEmail);
            _notifier.SetStatuCode(HttpStatusCode.NotFound);
            return false;
        }

        studentId = studentRegistered.Id;
        var bookBelongToTheCourseCategory = await _unitOfWork.Books.BookBelongToTheCourseCategoryAsync(request.BookId, request.StudentEmail);
        if (bookBelongToTheCourseCategory is null)
        {
            _notifier.AddError("Id", Errors.TheBookDoesNotBelongToTheCourseCategory, request.BookId);
            _notifier.SetStatuCode(HttpStatusCode.Forbidden);
            return false;
        }

        if (bookBelongToTheCourseCategory.IsLent)
        {
            _notifier.AddError("Id", Errors.BookAlreadyLent, request.BookId);
            _notifier.SetStatuCode(HttpStatusCode.Forbidden);
            return false;
        }

        return true;
    }
}
