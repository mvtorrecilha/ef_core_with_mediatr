using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Library.Repository.EntityConfigurations;


public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        builder.ToTable("Book");

        builder
           .HasKey(b => b.Id)
           .IsClustered(false)
           .HasName("PK_Book");

        builder.Property(b => b.Title)
            .HasColumnType("nvarchar(150)")
            .IsRequired(true);

        builder.Property(b => b.Author)
            .HasColumnType("nvarchar(100)")
            .IsRequired(true);

        builder.Property(b => b.Pages)
            .HasColumnType("int")
            .IsRequired(true);

        builder.Property(b => b.Publisher)
            .HasColumnType("nvarchar(300)")
            .IsRequired(true);

        builder.HasOne(b => b.Category)
           .WithMany(p => p.Books)
           .HasForeignKey(b => b.CategoryId)
           .IsRequired()
           .HasConstraintName("FK_Category")
           .OnDelete(DeleteBehavior.Restrict);

        builder.Property(b => b.IsLent)
           .IsRequired(true)
           .HasDefaultValue(false);
    }
}

