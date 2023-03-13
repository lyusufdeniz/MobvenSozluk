using MobvenSozluk.Repository.DTOs.RequestDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;

namespace MobvenSozluk.Repository.Services
{
    public interface IFilteringService<T>
    {
        public FilterResult FilterResult();
        public IEnumerable<T> GetFilteredData(IEnumerable<T> data,IEnumerable<FilterDTO> filters);

    }
}
