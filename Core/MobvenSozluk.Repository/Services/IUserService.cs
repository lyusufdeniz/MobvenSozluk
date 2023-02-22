using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.Services
{
    public interface IUserService: IService<User>
    {
        Task<CustomResponseDto<List<UsersWithRoleDto>>> GetUsersWithRole();
        Task<CustomResponseDto<UserByIdWithEntriesDto>> GetUserByIdWithEntries(int userId);
    }
}
