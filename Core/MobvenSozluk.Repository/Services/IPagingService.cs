using MobvenSozluk.Repository.DTOs.ResponseDTOs;

namespace MobvenSozluk.Repository.Services
{
    public interface IPagingService<T> where T : class
    {
    
        public IEnumerable<T> PageData(IEnumerable<T> items ,int pageNumber, int pageSize, out PagingResult pagingResult);


    }
}
