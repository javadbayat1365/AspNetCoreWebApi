using Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Post;
//[Table(name:"MainCategory",Schema ="post")]
public class Category:IEntity
{
    [Column(name: "Id")]
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }

    public Guid? ParentCategoryId { get; set; }
    [ForeignKey(nameof(ParentCategoryId))]
    public Category  ParentCategory { get; set; }

    public ICollection<Category>  categories { get; set; }

    public ICollection<Post>  posts { get; set; }
}
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> modelBuilder)
    {
        modelBuilder.HasOne(p => p.ParentCategory).WithMany(p => p.categories).HasForeignKey(p => p.ParentCategoryId);
        modelBuilder.Property(p => p.CategoryId).HasColumnName<Guid>("Id").IsRequired();
        //modelBuilder.Property(p => p.Name).HasMaxLength(100);//.IsRequired();
    }
}
