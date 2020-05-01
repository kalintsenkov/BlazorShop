namespace SheryLady.Data
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Interfaces;
    using Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Deal> Deals { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrdersProducts { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in this.ChangeTracker.Entries<IAuditInfo>())
            {
                _ = entry.State switch
                {
                    EntityState.Added => entry.Entity.CreatedOn = DateTime.Now,
                    EntityState.Modified => entry.Entity.ModifiedOn = DateTime.Now
                };
            }

            foreach (var entry in this.ChangeTracker.Entries<IDeletableEntity>())
            {
                if (entry.State != EntityState.Deleted)
                {
                    continue;
                }

                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedOn = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(DataSettings.DefaultConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}