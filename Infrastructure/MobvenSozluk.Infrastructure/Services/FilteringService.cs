using MobvenSozluk.Repository.DTOs.RequestDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using System.Linq.Expressions;

namespace MobvenSozluk.Repository.Services
{
    public class FilteringService<T> : IFilteringService<T>
    {
        private FilterResult filterresult;
        public FilterResult FilterResult()
        {
            return filterresult;
        }

        public IEnumerable<T> GetFilteredData(IEnumerable<T> data,IEnumerable<FilterDTO> filters)
        {
            var query = data.AsQueryable();
            List<FilterDTO> appliedfilters=new List<FilterDTO>();
            foreach (var filter in filters)
            {
                var parameter = Expression.Parameter(typeof(T), "p");
                var left = Expression.PropertyOrField(parameter, filter.PropertyName);
                var right = Expression.Constant(Convert.ChangeType(filter.Value, left.Type));
                var operation = filter.Operation.ToLower();
                Expression comparison;

                switch (operation)
                {
                    case "equals":
                        comparison = Expression.Equal(left, right);
                        appliedfilters.Add(filter);
                        break;
                    case "contains":
                        comparison = Expression.Call(left, "Contains", null, right);
                        appliedfilters.Add(filter);
                        break;
                    case "startswith":
                        comparison = Expression.Call(left, "StartsWith", null, right);
                        appliedfilters.Add(filter);
                        break;
                    case "endswith":
                        comparison = Expression.Call(left, "EndsWith", null, right);
                        appliedfilters.Add(filter);
                        break;
                    default:
                        continue;
                }
                filterresult = new FilterResult { Filters = appliedfilters };
                  var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);

                query = query.Where(lambda);
            }


            return query.ToList();
        }
    }
}
