using Library.Application.Notifications;
using MediatR;

namespace Library.Application.EventHandlers;

public class LogEventHandler : INotificationHandler<BorrowedBookNotification>
{
    public Task Handle(BorrowedBookNotification notification, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            Console.WriteLine($"Borrowed book: '{notification.BookId} - {notification.StudentEmail}'");
        });
    }
}
