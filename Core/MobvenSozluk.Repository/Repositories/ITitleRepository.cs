using MobvenSozluk.Domain.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.Repositories
{
    public interface ITitleRepository: IGenericRepository<Title>
    {
        Task<List<Title>> GetTitlesWithUserAndCategory();
        Task<Title> GetTitleByIdWithEntries(int titleId);
    }
}
