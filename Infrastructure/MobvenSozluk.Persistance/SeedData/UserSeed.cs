using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using MobvenSozluk.Domain.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Persistance.SeedData
{
    internal class UserSeed : IEntityTypeConfiguration<User>
    {
      
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<User>();
            builder.HasData(
                new User {
                    Id = 1,
                    Email = "admin@mobven.com",
                    NormalizedEmail = "admin@mobven.com".ToUpper().Normalize(),
                    UserName = "Admin",
                    NormalizedUserName = "Admin".ToUpper().Normalize(),
                    PasswordHash = hasher.HashPassword(null, "Pa$$w0rd"),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new User {
                    Id = 2,
                    Email = "editor@mobven.com",
                    NormalizedEmail = "editor@mobven.com".ToUpper().Normalize(),
                    UserName = "Editor",
                    NormalizedUserName = "Editor".ToUpper().Normalize(),
                    PasswordHash = hasher.HashPassword(null, "Pa$$w0rd"),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new User {
                    Id = 3,
                    Email = "user@mobven.com",
                    NormalizedEmail = "user@mobven.com".ToUpper().Normalize(),
                    UserName = "User",
                    NormalizedUserName = "User".ToUpper().Normalize(),
                    PasswordHash = hasher.HashPassword(null, "Pa$$w0rd"),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString(),
                }
                );
        }
    }
}
