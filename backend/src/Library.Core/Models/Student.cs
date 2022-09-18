using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Library.Core.Models;

[Table("Student")]
public class Student : BaseEntity
{
    public string Name { get; set; }

    public string Email { get; set; }

    public Guid CourseId { get; set; }

    public Course Course { get; set; }

    public ICollection<BorrowHistory> BorrowHistories { get; set; }
}
