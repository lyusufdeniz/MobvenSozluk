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
        private const double THIRTY_PERCENT = 0.3;
        private const int FIRST_TWENTY = 20;
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
                title.Views++;
                await _context.TitleView.AddAsync(titleView);
                await _context.SaveChangesAsync();
            }
            else
            {
                var existingViewWithSameIp = existingViews.FirstOrDefault(x => x.IpAddress == ipAddress);
                if (existingViewWithSameIp == null)
                {
                    title.Views++;
                    await _context.TitleView.AddAsync(titleView);
                    await _context.SaveChangesAsync();
                }
            }

            return title;
        }
        
        public async Task<List<Title>> GetPopularTitlesWithEntries()
        {
            var currentDate = DateTime.UtcNow;
            var startDate = currentDate.AddDays(-7);
            
            var popularTitles = await _context.Titles
                .Include(t => t.Entries)
                .Where(t => t.IsActive && !t.IsDeleted && t.CreatedDate >= startDate && t.Views >= 0)
                .OrderByDescending(t => (t.Views * THIRTY_PERCENT) + (t.Entries.Count * THIRTY_PERCENT) + (t.Entries.Sum(e => e.UpVotes) * THIRTY_PERCENT))
                .Take(FIRST_TWENTY)
                .ToListAsync();

            if (popularTitles.Count == 0)
            {
                popularTitles = await _context.Titles
                    .Include(t => t.Entries)
                    .Where(t => t.IsActive && !t.IsDeleted && t.Views >= 0)
                    .OrderByDescending(t => (t.Views * THIRTY_PERCENT) + (t.Entries.Count * THIRTY_PERCENT) + (t.Entries.Sum(e => e.UpVotes) * THIRTY_PERCENT))
                    .Take(FIRST_TWENTY)
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
