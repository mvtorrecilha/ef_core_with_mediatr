using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Library.Repository.EntityConfigurations;

public class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        builder.ToTable("Course");

        builder
            .HasKey(s => s.Id)
            .IsClustered(false)
            .HasName("PK_Course");

        builder.Property(s => s.Name)
            .HasColumnType("nvarchar(150)")
            .IsRequired(true);

        builder.HasMany(c => c.Students)
            .WithOne(p => p.Course)
            .HasForeignKey(s => s.CourseId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Students)
            .WithOne(p => p.Course)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
