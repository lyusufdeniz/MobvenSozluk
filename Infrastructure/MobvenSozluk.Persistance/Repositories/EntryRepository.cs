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
    public class EntryRepository : GenericRepository<Entry>, IEntryRepository
    {
        public EntryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Entry>> GetEntriesWithUserAndTitle()
        {
            return await _context.Entries.Include(x => x.User).Include(x => x.Title).ToListAsync();
        }

        public async Task<Entry> GetEntryByIdWithUserAndTitle(int entryId)
        {
            return await _context.Entries.Include(x => x.User).Include(x => x.Title).Where(x => x.Id == entryId).SingleOrDefaultAsync();
        }
    }
}
