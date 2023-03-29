using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;

namespace MobvenSozluk.Repository.Services
{
    public interface ICategoryService: IService<Category,CategoryDto>
    {
        Task<CustomResponseDto<CategoryByIdWithTitlesDto>> GetCategoryByIdWithTitles(int categoryId);
    }
}
