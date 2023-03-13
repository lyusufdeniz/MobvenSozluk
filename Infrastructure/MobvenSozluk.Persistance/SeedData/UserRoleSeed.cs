using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobvenSozluk.Domain.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Persistance.SeedData
{
    internal class UserRoleSeed : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(
              new UserRole
              {
                  UserId = 1,
                  RoleId = 1,
              },
              new UserRole
              {
                  UserId = 1,
                  RoleId = 2,
              },
              new UserRole
              {
                  UserId = 1,
                  RoleId = 3,
              },
              new UserRole
              {
                  UserId = 2,
                  RoleId = 2,
              },
              new UserRole
              {
                  UserId = 2,
                  RoleId = 3,
              },
              new UserRole
              {
                  UserId = 3,
                  RoleId = 3,
              }
              );
        }
    }
}
