using AutoMapper;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Services;

namespace MobvenSozluk.Infrastructure.Services
{
    public class PagingService<T, TDto>:IPagingService<T,TDto> where T : class where TDto : class
    {
        private readonly IMapper _mapper;
        public PagingService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public PagedResult<TDto> GetPage(IEnumerable<T> items, int pageNumber, int pageSize)
        {
            var totalCount = items.Count();
            var pageItems = items.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var pageDto = _mapper.Map<IEnumerable<T>, IEnumerable<TDto>>(pageItems);
            var pagedResultDto = new PagedResult<TDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = pageDto
            };
            return pagedResultDto;
        }
    }
}