using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Core.Models;

[Table("BorrowHistory")]
public class BorrowHistory
{
    public Guid BookId { get; set; }

    public Book Book { get; set; }

    public Guid StudentId { get; set; }

    public Student Student { get; set; }

    public DateTime BorrowDate { get; set; }

    public DateTime? ReturnDate { get; set; }
}
