using MobvenSozluk.Domain.Attributes;
using MobvenSozluk.Repository.DTOs.RequestDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using System.Data;
using System.Linq.Expressions;

namespace MobvenSozluk.Repository.Services
{
    public class FilteringService<T> : IFilteringService<T>
    {
        private FilterResult filterresult = null;
        public FilterResult FilterResult()
        {
            return filterresult;
        }

        public IEnumerable<T> GetFilteredData(IEnumerable<T> data, IEnumerable<FilterDTO> filters)
        {
            var query = data.AsQueryable();
            if (filters.Count() != 0)
            {
                Type objType = typeof(T);


                List<FilterDTO> appliedfilters = null; ;
                foreach (var filter in filters)
                {
                    var obj = objType.GetProperty(filter.FilterField);
                    if (obj != null && Attribute.IsDefined(obj, typeof(FilterAttribute)) == true)
                    {
                        var parameter = Expression.Parameter(typeof(T), "x");
                        var left = Expression.PropertyOrField(parameter, filter.FilterField);
                        var right = Expression.Constant(Convert.ChangeType(filter.Value, left.Type));
                        var operation = filter.FilterType.ToLower();
                        Expression comparison;

                        switch (operation)
                        {
                            case "equals":
                                comparison = Expression.Equal(left, right);
                                appliedfilters.Add(filter);
                                break;
                            case "min":
                                comparison = Expression.GreaterThanOrEqual(left, right);
                                appliedfilters.Add(filter);
                                break;
                            case "max":
                                comparison = Expression.LessThanOrEqual(left, right);
                                appliedfilters.Add(filter);
                                break;

                            default:
                                continue;
                        }

                        var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);

                        query = query.Where(lambda);
                    }

                }

                filterresult = new FilterResult { Filters = appliedfilters };
                return query.ToList();
            }
            return query.ToList();

        }


    }
}
