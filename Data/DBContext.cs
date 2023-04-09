using Common.Utilities;
using Entities.Common;
using Entities.Post;
using Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AppDBContext:DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var EntitiesAssembly = typeof(BaseEntity).Assembly;

            #region RegisterAllEntities
                //public DbSet<User> Users { get; set; }
                modelBuilder.RegisterAllEntities<IEntity>(EntitiesAssembly);
            #endregion

            #region Add All Fluent Api

            //Add Fluent Api Manually

            //modelBuilder.Entity<User>().Property(p => p.Age>10).HasDefaultValue(20);
            modelBuilder.RegisterEntityTypeConfiguration(EntitiesAssembly);

            #endregion

            modelBuilder.AddRestrictDeleteBehaviorConvention();

            #region Change GUID IDs To SequentionalGUID

            //modelBuilder.Entity<Category>().Property(p => p.CategoryId).HasDefaultValueSql("NEWSEQUENTIALID()");
            modelBuilder.AddSequentialGuidForIdConvention();
            #endregion
        }
    }
}
