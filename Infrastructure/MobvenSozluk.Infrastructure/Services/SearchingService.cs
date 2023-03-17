using MobvenSozluk.Domain.Attributes;
using MobvenSozluk.Repository.Services;

namespace MobvenSozluk.Infrastructure.Services
{
    public class SearchingService<T> : ISearchingService<T> where T : class
    {
        public IEnumerable<T> Search(IEnumerable<T> items, string query)
        {
            var _data = items;
            var _query = query.ToLower();
            // Get all properties of type T
            var properties = typeof(T).GetProperties();

            // Filter the properties that have the Searchable attribute
            var searchableProperties = properties.Where(p => Attribute.IsDefined(p, typeof(SearchAttribute)));

            // Filter the data based on the search term and the searchable properties
            var filteredData = _data.Where(item =>
            {
                foreach (var property in searchableProperties)
                {
                    var value = property.GetValue(item)?.ToString().ToLower();
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

