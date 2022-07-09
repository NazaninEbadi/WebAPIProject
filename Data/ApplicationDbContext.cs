using Common.Utilities;
using Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    public class ApplicationDbContext:DbContext    //IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{

		}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer();
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			var enitiesAssembly = typeof(IEntity).Assembly;

			modelBuilder.RegisterAllEntities<IEntity>(enitiesAssembly);
			modelBuilder.RegisterEntityTypeConfiguration(enitiesAssembly);
			modelBuilder.AddRestrictDeleteBehaviorConvention();
			modelBuilder.AddSequentialGuidForIdConvention();
			modelBuilder.AddPluralizingTableNameConvention();


		}

        public override int SaveChanges()
        {
			CleanString();

			return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
			CleanString();

			return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            CleanString();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void CleanString()
        {
			var changedEntities = ChangeTracker.Entries()
				.Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var item in changedEntities)
            {
				if (item.Entity == null)
					continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                             .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(String));

                foreach (var property in properties)
                {
                    var proName = property.Name;
                    var val = (string)property.GetValue(item.Entity,null);
                }

            }
        }
	}
}
