using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
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
        Task<CustomResponseDto<UserByIdWithTitlesDto>> GetUserByIdWithTitles(int userId);

        Task<CustomResponseDto<UserDto>> CreateAsync(AddUserDto userDto);
        Task<CustomResponseDto<UserDto>> EditAsync(UpdateUserDto userDto);
    }
}
