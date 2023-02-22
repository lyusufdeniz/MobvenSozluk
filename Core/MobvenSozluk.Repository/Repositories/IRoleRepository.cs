using MobvenSozluk.Domain.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.Repositories
{
    public interface IRoleRepository: IGenericRepository<Role>
    {
        Task<Role> GetRoleByIdWithUsers(int roleId);
    }
}
