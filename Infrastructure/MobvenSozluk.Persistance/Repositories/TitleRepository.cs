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

        public async Task<Title> GetTitleByIdWithEntries(int titleId, string ipAddress, int? userId)
        {
            var title = await _context.Titles.Include(x => x.Entries).SingleOrDefaultAsync(x => x.Id == titleId);
            if (title == null)
            {
                return null;
            }

            var titleView = new TitleView
            {
                TitleId = titleId,
                UserId = userId ?? 0,
                IpAddress = ipAddress,
                VisitDate = DateTime.UtcNow
            };

            var existingViews = await _context.TitleView.Where(x => x.TitleId == titleId && x.UserId == (userId ?? 0)).ToListAsync();

            if (existingViews.Count == 0)
            {
                // This is the first time the user has viewed this title
                title.Views++;
                await _context.TitleView.AddAsync(titleView);
                await _context.SaveChangesAsync();
            }
            else
            {
                // The user has viewed this title before
                var existingViewWithSameIp = existingViews.FirstOrDefault(x => x.IpAddress == ipAddress);
                if (existingViewWithSameIp == null)
                {
                    // The user is viewing the title with a different IP address than before
                    title.Views++;
                    await _context.TitleView.AddAsync(titleView);
                    await _context.SaveChangesAsync();
                }
                // else: The user is viewing the title with the same IP address as before, do nothing
            }

            return title;
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
