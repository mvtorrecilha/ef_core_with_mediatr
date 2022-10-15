using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Library.Repository.EntityConfigurations;

public class CourseCategoryEntityTypeConfigurations : IEntityTypeConfiguration<CourseCategory>
{
    public void Configure(EntityTypeBuilder<CourseCategory> builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        builder.ToTable("CourseCategory");

        builder.HasKey(cc => new { cc.CourseId, cc.CategoryId });

        builder
           .HasOne(cc => cc.Course)
           .WithMany(p => p.CourseCategories)
           .HasForeignKey(cc => cc.CourseId);

        builder
           .HasOne(cc => cc.Category)
           .WithMany(p => p.CourseCategories)
           .HasForeignKey(cc => cc.CategoryId);
    }
}