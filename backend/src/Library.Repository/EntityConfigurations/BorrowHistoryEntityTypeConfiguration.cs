using Library.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Library.Repository.EntityConfigurations;


public class BorrowHistoryEntityTypeConfiguration : IEntityTypeConfiguration<BorrowHistory>
{
    public void Configure(EntityTypeBuilder<BorrowHistory> builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        builder.ToTable("BorrowHistory");

        builder
            .HasKey(s => s.Id)
            .IsClustered(false)
            .HasName("PK_BorrowHistory");

        builder.HasOne(bh => bh.Book)
            .WithMany(b => b.BorrowHistories)
            .HasForeignKey(bh => bh.BookId)
            .IsRequired()
            .HasConstraintName("FK_Book")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(bh => bh.Student)
            .WithMany(b => b.BorrowHistories)
            .HasForeignKey(bh => bh.StudentId)
            .IsRequired()
            .HasConstraintName("FK_Student")
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(b => b.BorrowDate)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(b => b.ReturnDate)
            .HasColumnType("datetime")
            .IsRequired(false);
    }
}

