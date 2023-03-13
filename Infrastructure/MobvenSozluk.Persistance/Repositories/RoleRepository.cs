using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Persistance.Context;
using MobvenSozluk.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Persistance.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        
        public RoleRepository(AppDbContext context) : base(context)
        {
            
        }

        public async Task<Role> GetRoleByIdWithUsers(int roleId)
        {
            return await _context.Roles.Include(r => r.UserRoles).ThenInclude(ur => ur.User)
            .FirstOrDefaultAsync(r => r.Id == roleId);
        }
    }
}
