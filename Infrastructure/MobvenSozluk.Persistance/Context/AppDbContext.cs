using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MobvenSozluk.Domain.Concrete.Entities;
using System.Reflection;

namespace MobvenSozluk.Persistance.Context
{
    #region CODE EXPLANATION SECTION
    /*
      Managing database tables with Identity library.
      Created 3 different entity in "MobvenSozluk.Domain.Concrete.Entities" which are Role, User and UserRole tables.
        * Inherited all these tables from identity tables. Please check these 3 tables to understand how they inherited from Identity tables.
      AppDbContext needs Identity tables so in here AppDbContext has inherited from all these tables.
      In DbSet section; as you see there is no any configuration for user role etc. Because Identity library automatically creates tables after succeed migration with these inheritances.
     */
    #endregion
    public class AppDbContext : IdentityDbContext<User, Role, int,
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Title> Titles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());     
        }


    }
}
