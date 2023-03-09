using MobvenSozluk.Repository.DTOs.ResponseDTOs;

namespace MobvenSozluk.Repository.Services
{
    public interface IPagingService<T> where T : class
    {

        public  Tuple<PagingResult, IEnumerable<T>> GetPaged(IEnumerable<T> items, int pageNumber, int pageSize);


    }
}
