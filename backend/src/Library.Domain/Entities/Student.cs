using Dapper.Contrib.Extensions;

namespace Library.Domain.Entities;

[Table("Student")]
public class Student : BaseEntity
{
    public string Name { get; set; }

    public string Email { get; set; }

    public Guid CourseId { get; set; }

    public Course Course { get; set; }

    public ICollection<BorrowHistory> BorrowHistories { get; set; }
}
