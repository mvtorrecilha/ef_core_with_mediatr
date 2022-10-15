namespace Library.Infra.ResponseNotifier.Common;

public static class Errors
{
    public const string
        EmailCannotBeEmpty = "Email cannot be empty.",
        StudentNotFound = "Student not found in database by email.",
        TheBookDoesNotBelongToTheCourseCategory = "The book does not belong to the course category.",
        BookAlreadyLent = "The book is already lent.";
}
