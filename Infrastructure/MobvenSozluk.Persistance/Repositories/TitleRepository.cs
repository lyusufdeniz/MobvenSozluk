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
    public class TitleRepository : GenericRepository<Title>, ITitleRepository
    {
        public TitleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Title> GetTitleByIdWithEntries(int titleId)
        {
            return await _context.Titles.Include(x => x.Entries).Where(x => x.Id == titleId).SingleOrDefaultAsync();
        }

        public async Task<List<Title>> GetTitlesWithUserAndCategory()
        {
          return await _context.Titles.Include(x => x.User).Include(z => z.Category).ToListAsync();
        }
    }
}
