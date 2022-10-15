using Library.Domain.Entities;
using Library.Repository.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.Repository.Context;

public class LibraryContext : DbContext
{

    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<CourseCategory> CourseCategories { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Student> Students { get; set; }

    public DbSet<BorrowHistory> BorrowHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(nameof(modelBuilder));

        modelBuilder
            .ApplyConfiguration(new CategoryEntityTypeConfiguration())
            .ApplyConfiguration(new CourseCategoryEntityTypeConfigurations())
            .ApplyConfiguration(new CourseEntityTypeConfiguration())
            .ApplyConfiguration(new StudentEntityTypeConfiguration())
            .ApplyConfiguration(new BookEntityTypeConfiguration())
            .ApplyConfiguration(new BorrowHistoryEntityTypeConfiguration());
    }
}
