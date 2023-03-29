using MobvenSozluk.Domain.Attributes;
using MobvenSozluk.Domain.Constants;
using MobvenSozluk.Repository.DTOs.RequestDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using System.Data;
using System.Linq.Expressions;

namespace MobvenSozluk.Repository.Services
{
    /// <summary>
    /// Data Filtering Service
    /// </summary>
    /// <typeparam name="T">T is generic entity type</typeparam>
    public class FilteringService<T> : IFilteringService<T>
    {
        /// <summary>
        /// Get Filtered Data
        /// </summary>
        /// <param name="data">Data to apply filters</param>
        /// <param name="filters">Filters List</param>
        /// <param name="filterResult">Result of applied filters</param>
        /// <returns>Filtered Data and Filter Results</returns>
        public IEnumerable<T> GetFilteredData(IEnumerable<T> data, IEnumerable<FilterDTO> filters, out FilterResult? filterResult)
        {
            var query = data.AsQueryable();
            List<FilterDTO> appliedfilters = new List<FilterDTO>();
            if (filters.Any())
            {
                Type objType = typeof(T);
            
                foreach (var filter in filters)
                {
                    var obj = objType.GetProperty(filter.FilterField);
                    if (obj != null && Attribute.IsDefined(obj, typeof(FilterAttribute)) == true)
                    {
                        //generate dynamic lambda expression 
                        var parameter = Expression.Parameter(typeof(T), "x");
                        var parameterOfCondition = Expression.PropertyOrField(parameter, filter.FilterField);
                        var condition = Expression.Constant(Convert.ChangeType(filter.Value, parameterOfCondition.Type));
                        var operation = filter.FilterType.ToLower();
                        Expression comparison;

                        switch (operation)
                        {
                            case MagicStrings.FilterCaseEqual:
                                comparison = Expression.Equal(parameterOfCondition, condition);
                                appliedfilters.Add(filter);
                                break;
                            case MagicStrings.FilterCaseMinimum:
                                comparison = Expression.GreaterThanOrEqual(parameterOfCondition, condition);
                                appliedfilters.Add(filter);
                                break;
                            case MagicStrings.FilterCaseMaximum:
                                comparison = Expression.LessThanOrEqual(parameterOfCondition, condition);
                                appliedfilters.Add(filter);
                                break;

                            default:
                                continue;
                        }

                        var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);

                        query = query.Where(lambda);
                    }

                }
                if(appliedfilters.Any())

                {
                    filterResult = new FilterResult { Filters = appliedfilters };
                    return query.ToList();
                }
               
            }
            filterResult = null;

            return query.ToList();

        }
        


    }
}
