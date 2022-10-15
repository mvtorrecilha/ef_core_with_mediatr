using MediatR;

namespace Library.Application.Commands;

public class BorrowBookCommand : IRequest
{
    public Guid BookId { get; set; }
    public string StudentEmail { get; set; }
}
