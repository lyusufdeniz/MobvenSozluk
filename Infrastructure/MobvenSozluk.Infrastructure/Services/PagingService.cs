using Microsoft.AspNetCore.Mvc.RazorPages;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Services;

namespace MobvenSozluk.Infrastructure.Services
{
    public class PagingService<T> : IPagingService<T> where T : class
    {

        public IEnumerable<T> PageData(IEnumerable<T> items, int pageNumber, int pageSize,out PagingResult pagingResult)
        {
            int _pageNumber = (pageNumber < 1) ? 1 : pageNumber;
            int _pageSize = (pageSize < 1 || pageSize > 50) ? 50 : pageSize;
            var _items = items;
            var totalCount = items.Count();
            pagingResult = new PagingResult
            {

                PageNumber = _pageNumber,
                PageSize = _pageSize,
                TotalCount = totalCount
            };
            var pageItems = _items.Skip((_pageNumber - 1) * _pageSize).Take(_pageSize).AsEnumerable();
            return pageItems;
        }




    }

}
