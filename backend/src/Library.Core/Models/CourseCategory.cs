using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Core.Models;

[Table("CourseCategory")]
public class CourseCategory : BaseEntity
{
    public Guid CourseId { get; set; }

    public Course Course { get; set; }

    public Guid CategoryId { get; set; }

    public Category Category { get; set; }
}
