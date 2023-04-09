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
        //[MaxLength(100)]
        public string FullName { get; set; }
        public int Age { get; set; }
        public GenderEnum Gender { get; set; }

        public ICollection<Post>  posts { get; set; }
    }
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.HasMany(p => p.posts).WithOne(p => p.User).HasForeignKey(p => p.AuthorId);
            modelBuilder.Property(p => p.Age).HasMaxLength(100);
            modelBuilder.Property(p => p.FullName).HasMaxLength(100).IsRequired();
        }
    }
}
