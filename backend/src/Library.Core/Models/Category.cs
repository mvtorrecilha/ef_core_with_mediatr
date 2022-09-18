using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Core.Models;

[Table("Category")]
public class Category : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Book> Books { get; set; }

    public ICollection<CourseCategory> CourseCategories { get; set; }
}
