using MobvenSozluk.Domain.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.Repositories
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<List<User>> GetUsersWithRole();
        Task<User> GetUserByIdWithEntries(int userId);
        Task<User> GetUserByIdWithTitles(int userId);
        Task<User> GetUserByIdWithRoles(User user);
    }
}
