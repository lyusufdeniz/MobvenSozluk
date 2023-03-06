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

        public async Task<List<Title>> GetPopularTitlesWithEntries()
        {
            var currentDate = DateTime.UtcNow;
            var startDate = currentDate.AddDays(-7);

            // Get the titles with their related entries, filtering by activity in the last 7 days
            var popularTitles = await _context.Titles
                .Include(t => t.Entries)
                .Where(t => t.IsActive && !t.IsDeleted && t.CreatedDate >= startDate && t.Views >= 0)
                .OrderByDescending(t => (t.Views * 0.3) + (t.Entries.Count * 0.4) + (t.Entries.Sum(e => e.UpVotes) * 0.3))
                .Take(20)
                .ToListAsync();

            // If there is no activity in the last 7 days, get the 20 most popular titles regardless of time
            if (popularTitles.Count == 0)
            {
                popularTitles = await _context.Titles
                    .Include(t => t.Entries)
                    .Where(t => t.IsActive && !t.IsDeleted && t.Views >= 0)
                    .OrderByDescending(t => (t.Views * 0.3) + (t.Entries.Count * 0.4) + (t.Entries.Sum(e => e.UpVotes) * 0.3))
                    .Take(20)
                    .ToListAsync();
            }

            return popularTitles;
        }


        public async Task<List<Title>> GetTitlesWithUserAndCategory()
        {
          return await _context.Titles.Include(x => x.User).Include(z => z.Category).ToListAsync();
        }
    }
}
