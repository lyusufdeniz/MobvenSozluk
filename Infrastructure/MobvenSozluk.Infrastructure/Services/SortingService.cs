using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Services;
using System.Globalization;

namespace MobvenSozluk.Infrastructure.Services
{
    public class SortingService<T> : ISortingService<T>
    {
        private SortingResult sortingParameters;
    
        public IEnumerable<T> SortData(IEnumerable<T> items, bool sortByDesc, string sortParameter)
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
            sortingParameters = new SortingResult { SortByDesc = sortByDesc, SortParameter = _sortParameter };
            return items;
        }

        public SortingResult SortingParameter()
        {
            return sortingParameters;
        }


        // Helper method to get the value of a property by name using reflection
        private object GetPropertyValue(object obj, string propertyName)
        {
            var property = char.ToUpper(propertyName[0]) + propertyName.Substring(1);


            return obj.GetType().GetProperty(property)?.GetValue(obj, null);
        }
    }
    }
    
