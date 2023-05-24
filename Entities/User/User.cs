using Common.Enums;
using Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.User
{
    public class User:BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        [Required]
        [StringLength(500)]
        public string PasswordHash { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public Guid SecurityStamp { get; set; }

        public ICollection<Post.Post>  posts { get; set; }
    }
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        { 
            modelBuilder.HasMany(p => p.posts).WithOne(p => p.User).HasForeignKey(p => p.AuthorId);
            modelBuilder.Property(p => p.Age).HasMaxLength(2);
            modelBuilder.Property(p => p.FullName).HasMaxLength(100).IsRequired();
        }
    }
}
