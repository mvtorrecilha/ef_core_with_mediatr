using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Entities;

[Table("Course")]
public class Course : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Student> Students { get; set; }

    public ICollection<CourseCategory> CourseCategories { get; set; }

}
