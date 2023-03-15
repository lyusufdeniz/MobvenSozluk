using MobvenSozluk.Domain.Attributes;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Services;

namespace MobvenSozluk.Infrastructure.Services
{
    public class SortingService<T> : ISortingService<T>
    {
        private SortingResult sortingParameters;
        private string shortField = "Id";

        public IEnumerable<T> SortData(IEnumerable<T> items, bool sortByDesc, string sortParameter)
        {

            if (String.IsNullOrEmpty(sortParameter))
            {
                sortParameter = shortField;
            }

            if (sortByDesc == true)
            {
                items = items.OrderByDescending(x => GetPropertyValue(x, sortParameter));
            }
            else
            {
                items = items.OrderBy(x => GetPropertyValue(x, sortParameter));
            }
            sortingParameters = new SortingResult { SortByDesc = sortByDesc, SortParameter = shortField };
            return items;
        }

        public SortingResult SortResult()
        {
            return sortingParameters;
        }


        // Helper method to get the value of a property by name using reflection
        private object GetPropertyValue(object obj, string propertyName)
        {
            string _propertyName = char.ToUpper(propertyName[0]) + propertyName.Substring(1);

            var property = obj.GetType().GetProperty(_propertyName);
            if (property != null)
            {
                if (Attribute.IsDefined(property, typeof(SortAttribute)) == true)
                {
                    shortField = _propertyName;
                    return property;
                }
            }
        
            return obj.GetType().GetProperty("Id")?.GetValue(obj, null);




        }
    }
}

