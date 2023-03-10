using MobvenSozluk.Domain.Concrete;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;

namespace MobvenSozluk.Repository.Services
{
    public interface ISortingService<T>
    {
        public SortingResult SortingParameter();
        public IEnumerable<T> SortData(IEnumerable<T> items, bool sortByDesc, string sortParameter);
    }
}
