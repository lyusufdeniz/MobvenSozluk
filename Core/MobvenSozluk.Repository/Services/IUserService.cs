using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;

namespace MobvenSozluk.Repository.Services
{
    public interface IUserService: IService<User,UserDto>
    {
        #region CODE EXPLANATION SECTION
        /*
          Create CreateAsync and EditAsync to handle bussiness codes.
          Normally GenericService has add and edit methods but it is too weak to handle create and edit users because for now program
              uses the identity library; this means more specific code will be needed than generic.
         */
        #endregion

        Task<CustomResponseDto<List<UsersWithRoleDto>>> GetUsersWithRole();
        Task<CustomResponseDto<UserByIdWithEntriesDto>> GetUserByIdWithEntries(int userId);
        Task<CustomResponseDto<UserByIdWithTitlesDto>> GetUserByIdWithTitles(int userId);

        Task<CustomResponseDto<UserDto>> CreateAsync(AddUserDto userDto);
        Task<CustomResponseDto<UserDto>> EditAsync(UpdateUserDto userDto);
    }
}
