using MobvenSozluk.Repository.DTOs.ResponseDTOs;

namespace MobvenSozluk.Repository.Services
{
    public interface IPagingService<T,TDto> where T : class where  TDto: class
    {
        PagedResult<TDto> GetPage(IEnumerable<T> items, int pageNumber, int pageSize);
    }
}