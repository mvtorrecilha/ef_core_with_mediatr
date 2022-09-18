using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace Library.Core.Models;

[Table("Book")]
public class Book : BaseEntity
{
    public string Title { get; set; }

    public string Author { get; set; }

    public int Pages { get; set; }

    public string Publisher { get; set; }

    public Guid CategoryId { get; set; }

    public Category Category { get; set; }

    public bool IsLent { get; set; }

    public ICollection<BorrowHistory> BorrowHistories { get; set; }
}
