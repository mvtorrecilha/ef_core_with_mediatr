using MediatR;

namespace Library.Application.Notifications;

public class BorrowedBookNotification : INotification
{
    public Guid BookId { get; set; }
    public string StudentEmail { get; set; }
}
