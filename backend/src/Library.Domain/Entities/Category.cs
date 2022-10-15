using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Entities;

[Table("Category")]
public class Category : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Book> Books { get; set; }

    public ICollection<CourseCategory> CourseCategories { get; set; }
}
