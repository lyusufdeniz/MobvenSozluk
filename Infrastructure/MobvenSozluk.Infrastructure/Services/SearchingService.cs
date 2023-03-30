using MobvenSozluk.Domain.Attributes;
using MobvenSozluk.Domain.Constants;
using MobvenSozluk.Infrastructure.Exceptions;
using MobvenSozluk.Repository.Services;

namespace MobvenSozluk.Infrastructure.Services
{
    /// <summary>
    /// Generic Searching service
    /// </summary>
    /// <typeparam name="T">T is Generic Entity Type</typeparam>
    public class SearchingService<T> : ISearchingService<T> where T : class
    {
        /// <summary>
        /// Search items in given data list with given query string with reflection
        /// </summary>
        /// <param name="items">Data for searching</param>
        /// <param name="query">Query keyword</param>
        /// <returns></returns>
        public IEnumerable<T> Search(IEnumerable<T> items, string query)
        {
        
            var _data = items;
            if(query == null)
            {
                throw new BadRequestException(MagicStrings.SearchKeywordCantBeNull);
            }
            var _query = query.ToLower();
      
            var properties = typeof(T).GetProperties();

        
            var searchableProperties = properties.Where(p => Attribute.IsDefined(p, typeof(SearchAttribute)));

        
            var filteredData = _data.Where(item =>
            {
                foreach (var property in searchableProperties)
                {
         
    
                    var value = property.GetValue(item)?.ToString()?.ToLower();
                    if (value != null && value.Contains(_query))
                    {
                        return true;
                    }
                }
                return false;
            });

            return filteredData;
        }
    }
}

