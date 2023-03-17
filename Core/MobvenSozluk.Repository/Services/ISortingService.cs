using MobvenSozluk.Repository.DTOs.ResponseDTOs;

namespace MobvenSozluk.Repository.Services
{
    public interface ISortingService<T>
    {
        public SortingResult SortResult();
        public IEnumerable<T> SortData(IEnumerable<T> items, bool sortByDesc, string sortParameter);
    }
}
