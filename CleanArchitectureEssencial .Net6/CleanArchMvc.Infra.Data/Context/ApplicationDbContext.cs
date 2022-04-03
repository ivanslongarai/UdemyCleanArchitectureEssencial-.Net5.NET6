using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CleanArchMvc.Infra.Data.EntitiesConfiguration;
using Flunt.Notifications;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CleanArchMvc.Infra.Data.Identity;

namespace CleanArchMvc.Infra.Data.Context
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // It could also be like this:
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Ignore<Notification>();
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

    }
}