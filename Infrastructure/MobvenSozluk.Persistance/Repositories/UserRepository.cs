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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserByIdWithEntries(int userId)
        {
            return await _context.Users.Include(x => x.Entries).Where(x => x.Id == userId).SingleOrDefaultAsync();
        }

        public async Task<User> GetUserByIdWithTitles(int userId)
        {
            return await _context.Users.Include(x => x.Titles).Where(x => x.Id == userId).SingleOrDefaultAsync();
        }

        public async Task<List<User>> GetUsersWithRole()
        {

            return await _context.Users.Include(x => x.Role).ToListAsync();
        }
    }
}
