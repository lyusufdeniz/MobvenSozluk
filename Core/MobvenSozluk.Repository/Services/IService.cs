using MobvenSozluk.Repository.DTOs.RequestDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using System.Linq.Expressions;

namespace MobvenSozluk.Repository.Services
{
    public interface IService<T, TDto> where T : class
    {
        public Task<CustomResponseDto<TDto>> GetByIdAsync(int id);
        public Task<CustomResponseDto<List<TDto>>> Search(int pageNo, int pageSize,string searchTerm);
        public Task<CustomResponseDto<List<TDto>>> GetAllAsync(bool sortByDesc, string sortparameter, int pagenumber, int pageSize, List<FilterDTO> filters);
        public Task<CustomResponseDto<List<TDto>>> Where(Expression<Func<T, bool>> expression);
        public Task<CustomResponseDto<TDto>> AnyAsync(Expression<Func<T, bool>> expression);
        public Task<CustomResponseDto<TDto>> AddAsync(TDto entity);
        public Task<CustomResponseDto<List<TDto>>> AddRangeAsync(List<TDto> entities);
        public Task<CustomResponseDto<TDto>> UpdateAsync(TDto entity);
        public Task<CustomResponseDto<TDto>> RemoveAsync(int id);
        public Task<CustomResponseDto<List<TDto>>> RemoveRangeAsync(List<TDto> entities);
    }
}
