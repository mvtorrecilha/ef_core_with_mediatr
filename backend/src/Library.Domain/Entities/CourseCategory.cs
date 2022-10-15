using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Entities;

[Table("CourseCategory")]
public class CourseCategory
{
    public Guid CourseId { get; set; }

    public Course Course { get; set; }

    public Guid CategoryId { get; set; }

    public Category Category { get; set; }
}
