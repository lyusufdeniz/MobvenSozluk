using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;

namespace MobvenSozluk.Repository.Services
{
    public interface ITitleService: IService<Title,TitleDto>
    {
        
        Task<CustomResponseDto<List<TitlesWithUserAndCategoryDto>>> GetTitlesWithUserAndCategory();
        Task<CustomResponseDto<TitleByIdWithEntriesDto>> GetTitleByIdWithEntries(int titleId, string ipAddress, int? userId);
        Task<List<TitleDto>> GetPopularTitlesAsync();
    }
}
