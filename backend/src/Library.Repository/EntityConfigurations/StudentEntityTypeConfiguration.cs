using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Library.Repository.EntityConfigurations;

public class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        builder.ToTable("Student");

        builder
            .HasKey(s => s.Id)
            .IsClustered(false)
            .HasName("PK_Student");

        builder.Property(s => s.Id)
            .ValueGeneratedNever()
            .HasDefaultValueSql("newsequentialid()")
            .IsRequired();

        builder.Property(s => s.Name)
            .HasColumnType("nvarchar(150)")
            .IsRequired(true);

        builder.Property(s => s.Email)
            .HasColumnType("nvarchar(150)")
            .IsRequired(true);

        builder.HasOne(s => s.Course)
            .WithMany(s => s.Students)
            .HasForeignKey(s => s.CourseId)
            .IsRequired()
            .HasConstraintName("FK_Course")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
