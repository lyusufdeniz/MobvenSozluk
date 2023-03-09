using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Services;
using System.Globalization;

namespace MobvenSozluk.Infrastructure.Services
{
    public class SortingService<T> : ISortingService<T>
    {

        public Tuple<SortingResult, IEnumerable<T>> Sort(IEnumerable<T> items, bool sortByDesc=false, string sortParameter = "id")
        {
            string _sortParameter;
            if (String.IsNullOrEmpty(sortParameter))
            {
                _sortParameter = "Id";
            }
            else
                _sortParameter = sortParameter;

            if (sortByDesc == true)
            {
                items = items.OrderByDescending(x => GetPropertyValue(x, _sortParameter));
            }
            else
            {
                items = items.OrderBy(x => GetPropertyValue(x, _sortParameter));
            }

            return Tuple.Create(new SortingResult { SortByDesc= sortByDesc, SortParameter= _sortParameter }, items);
        }

        // Helper method to get the value of a property by name using reflection
        private object GetPropertyValue(object obj, string propertyName)
        {
           
            return obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
        }
    }
    }
    
