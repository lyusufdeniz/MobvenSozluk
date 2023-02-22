using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.Services
{
    public interface IEntryService: IService<Entry>
    {
        Task<CustomResponseDto<List<EntriesWithUserAndTitleDto>>> GetEntriesWithUserAndTitle();
        Task<CustomResponseDto<EntriesWithUserAndTitleDto>> GetEntryByIdWithUserAndTitle(int entryId);
    }
}
