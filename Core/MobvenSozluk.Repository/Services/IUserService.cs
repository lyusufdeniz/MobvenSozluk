using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;

namespace MobvenSozluk.Repository.Services
{
    public interface IUserService: IService<User,UserDto>
    {
        Task<CustomResponseDto<List<UsersWithRoleDto>>> GetUsersWithRole();
        Task<CustomResponseDto<UserByIdWithEntriesDto>> GetUserByIdWithEntries(int userId);
        Task<CustomResponseDto<UserByIdWithTitlesDto>> GetUserByIdWithTitles(int userId);
    }
}
