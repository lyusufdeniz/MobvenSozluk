using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobvenSozluk.Domain.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Persistance.SeedData
{
    internal class RoleSeed : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
               new Role
               {
                   Id = 1,
                   Name = "Admin",
                   NormalizedName = "ADMIN".ToUpper().Normalize()
               },
               new Role
               {
                   Id = 2,
                   Name = "Editor",
                   NormalizedName = "EDITOR".ToUpper().Normalize()
               },
               new Role
               {
                   Id = 3,
                   Name = "User",
                   NormalizedName = "USER".ToUpper().Normalize()
               }
               );
        }
    }
}