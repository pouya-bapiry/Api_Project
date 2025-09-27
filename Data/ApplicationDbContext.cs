using Common.Utilities;
using Entities.Common;
using Entities.Post;
using Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(IEntity).Assembly;

            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.AddSequentialGuidForIdConvention();
            modelBuilder.AddPluralizingTableNameConvention();
        }

        //#region Dbset

        //public DbSet<User> Users { get; set; }
        //public DbSet<Post> Posts { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<Category> Categories { get; set; }

        //#endregion


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{


        //    foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
        //    {
        //        relationship.DeleteBehavior = DeleteBehavior.Restrict;
        //    }

        //    base.OnModelCreating(modelBuilder);
        //}



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    var entitiesAssembly = typeof(IEntity).Assembly;
        //    modelBuilder.RegisterAllEntities<BaseEntity>(entitiesAssembly);
        //    modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
        //    modelBuilder.AddRestrictDeleteBehaviorConvention();
        //    modelBuilder.AddSequentialGuidForIdConvention();
        //    //اگر خواستیم برای فیلد دیگری هم جیو آیدی به ازای هر فیلد دلخواه بزاریم کار زیر رو انجام میدیم
        //    //modelBuilder.Entity<Post>().Property(p => p.Title).HasDefaultValueSql("NEWSEQUENTIALID()");
        //    modelBuilder.AddPluralizingTableNameConvention();
        //}
        public override int SaveChanges()
        {
            _cleanString();
            return base.SaveChanges();
        }

        #region CleanString

        

      
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            _cleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            _cleanString();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _cleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
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
        #endregion
    }
}
