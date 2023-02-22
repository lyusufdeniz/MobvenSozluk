using MobvenSozluk.Domain.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.Repositories
{
    public interface IEntryRepository: IGenericRepository<Entry>
    {
        Task<List<Entry>> GetEntriesWithUserAndTitle();
        Task<Entry> GetEntryByIdWithUserAndTitle(int entryId);

    }
}
