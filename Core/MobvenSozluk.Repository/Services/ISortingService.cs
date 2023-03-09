using MobvenSozluk.Domain.Concrete;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;

namespace MobvenSozluk.Repository.Services
{
    public interface ISortingService<T>
    {
   
        public Tuple<SortingResult, IEnumerable<T>> Sort(IEnumerable<T> items, bool sortByDesc, string sortParameter);
    }
}
