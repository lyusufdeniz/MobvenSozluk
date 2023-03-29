using MobvenSozluk.Domain.Attributes;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Services;

namespace MobvenSozluk.Infrastructure.Services
{/// <summary>
/// Data sorting service
/// </summary>
/// <typeparam name="T">T is generic entity type</typeparam>
    public class SortingService<T> : ISortingService<T>
    {
        /// <summary>
        /// Sort parameter
        /// </summary>
        private string shortField = "Id";
        /// <summary>
        /// Get Sorted data with given parameters 
        /// </summary>
        /// <param name="items">Data to sort will applying</param>
        /// <param name="sortByDesc">Sort Direction</param>
        /// <param name="sortParameter">Sortable Parameter Name</param>
        /// <param name="sortingResult">//Result to show sorting detail</param>
        /// <returns>IEnumerable Sorted Data</returns>
        public IEnumerable<T> SortData(IEnumerable<T> items, bool sortByDesc, string sortParameter, out SortingResult sortingResult)
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
            sortingResult = new SortingResult { SortByDesc = sortByDesc, SortParameter = sortParameter };
            return items;
        }


        /// <summary>
        ///  Helper method to get the value of a property by name using reflection
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="propertyName">property to get value</param>
        /// <returns></returns>

        private object GetPropertyValue(object obj, string propertyName)
        {
            string _propertyName = char.ToUpper(propertyName[0]) + propertyName.Substring(1);

            var property = obj.GetType().GetProperty(_propertyName);
            if (property != null)
            {
                if (Attribute.IsDefined(property, typeof(SortAttribute)) == true)
                {
                    shortField = _propertyName;
                    return property.GetValue(obj, null); ;
                }
            }
        
            return obj.GetType().GetProperty("Id")?.GetValue(obj, null);




        }
    }
}

