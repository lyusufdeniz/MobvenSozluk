using Microsoft.EntityFrameworkCore;
using MobvenSozluk.Domain.Concrete.Entities;
using System.Reflection;

namespace MobvenSozluk.Persistance.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Entry>? Entries { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<Title>? Titles { get; set; }
        public DbSet<User>? Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("SqlConnection");
            }
        }

    }
}