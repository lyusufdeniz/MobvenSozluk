using MobvenSozluk.Repository.DTOs.ResponseDTOs;

namespace MobvenSozluk.Repository.Services
{
    public interface ISortingService<T>
    {
        public IEnumerable<T> SortData(IEnumerable<T> items, bool sortByDesc, string sortParameter,out SortingResult sortingResult);
    }
}
