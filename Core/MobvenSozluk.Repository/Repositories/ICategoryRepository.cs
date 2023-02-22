using MobvenSozluk.Domain.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.Repositories
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        Task<Category> GetCategoryByIdWithTitles(int categoryId);
    }
}
