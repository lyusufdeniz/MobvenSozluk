using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;

namespace MobvenSozluk.Repository.Services
{
    public interface IEntryService: IService<Entry, EntryDto>
    {
        Task<CustomResponseDto<List<EntriesWithUserAndTitleDto>>> GetEntriesWithUserAndTitle();
        Task<CustomResponseDto<EntriesWithUserAndTitleDto>> GetEntryByIdWithUserAndTitle(int entryId);
    }
}
