using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobvenSozluk.Domain.Concrete.Entities;

namespace MobvenSozluk.Persistance.Seeds
{
    internal class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User { Id = 1, Email = "admin@mobven.com", Password = "Pa$$w0rd", RoleId = 1, Name = "Admin" }, 
                new User { Id = 2, Email = "editor@mobven.com", Password = "Pa$$w0rd", RoleId = 2, Name = "Editor" }, 
                new User { Id = 2, Email = "user@mobven.com", Password = "Pa$$w0rd", RoleId = 3, Name = "User" });
     
        }
    }
}
