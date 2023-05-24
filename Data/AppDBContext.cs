using Common.Utilities;
using Entities;
using Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AppDBContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public AppDBContext(DbContextOptions options):base(options)
        {

        }
        #region Connect To SqlServer 

        //private readonly IConfiguration configuration;
        //public AppDBContext(IConfiguration configuration)
        //{
        //    this.configuration = configuration;
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlServer"));// "Data Source=.;Initial Catalog=MyApiDB;Integrated Security = true");
        //    base.OnConfiguring(optionsBuilder);
        //}
        #endregion
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

            #region Pluralize Entities
            modelBuilder.AddPluralizingTableNameConvention(); // city => cities , person => people
            #endregion
        }
        public override int SaveChanges()
        {
            _cleanString();
            return base.SaveChanges();  
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _cleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }

        //~AppDBContext() => Console.WriteLine($"The {typeof(DbContext)} finilazer is excuting!");
    }
}
