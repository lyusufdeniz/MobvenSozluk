using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobvenSozluk.Repository.DTOs.EntityDTOs;

namespace MobvenSozluk.Repository.Services
{
    public interface ITitleService: IService<Title>
    {
        
        Task<CustomResponseDto<List<TitlesWithUserAndCategoryDto>>> GetTitlesWithUserAndCategory();
        Task<CustomResponseDto<TitleByIdWithEntriesDto>> GetTitleByIdWithEntries(int titleId);
        Task<List<TitleDto>> GetPopularTitlesAsync();
    }
}
