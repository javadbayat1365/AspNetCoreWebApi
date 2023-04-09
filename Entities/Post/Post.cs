using Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Entities.User;

namespace Entities.Post;

public class Post:BaseEntity<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTimeOffset CreateAt { get; } = DateTime.Now;

    public Guid CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public Category  Category { get; set; }

    public long AuthorId { get; set; }
    [ForeignKey(nameof(AuthorId))]
    public Entities.User.User  User { get; set; }
}
public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> modelBuilder)
    {
        //modelBuilder.HasOne(p => p.User).WithMany(p => p.posts).HasForeignKey(p => p.AuthorId);
        modelBuilder.HasOne(p => p.Category).WithMany(p => p.posts).HasForeignKey(p => p.CategoryId);
        modelBuilder.Property(p => p.Title).HasDefaultValue("عنوان مقاله").IsRequired().HasMaxLength(200);
        modelBuilder.Property<string>(p => p.Description).IsRequired().HasMaxLength(2000);
    }
}
