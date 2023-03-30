using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Domain.Constants;
using MobvenSozluk.Infrastructure.Exceptions;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Repositories;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Repository.UnitOfWorks;

namespace MobvenSozluk.Infrastructure.Services
{
    public class UserService : Service<User,UserDto>, IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUnitOfWork _unitOfWork;



        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper, IPagingService<User> pagingService, ISortingService<User> sortingService, IFilteringService<User> filteringService, UserManager<User> userManager, RoleManager<Role> roleManager, ISearchingService<User> searchingService) : base(repository, unitOfWork, sortingService, pagingService, mapper, filteringService, searchingService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<CustomResponseDto<UserDto>> CreateAsync(AddUserDto userDto)
        {
            var userExists = await _userManager.FindByEmailAsync(userDto.Email);
            if (userExists != null)
            {
                throw new NotFoundException(MagicStrings.UserAlreadyExist);
            }

            var user = new User
            {
                UserName = userDto.Name,
                Email = userDto.Email,
            };


            if(!await _roleManager.RoleExistsAsync(userDto.RoleName))
            {
                throw new NotFoundException(MagicStrings.RoleNotExist);
            }
          
            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                throw new BadRequestException(MagicStrings.BadRequestDescription);
            }
            

            switch (userDto.RoleName)
            {
                case "Admin":
                    await _userManager.AddToRolesAsync(user, new[] { "Admin", "Editor", "User" });
                    break;
                case "Editor":
                    await _userManager.AddToRolesAsync(user, new[] { "Editor", "User" });
                    break;
                case "User":
                    await _userManager.AddToRoleAsync(user, "User");
                    break;
                default:
                    await _userManager.AddToRoleAsync(user, userDto.RoleName);
                    await _userManager.AddToRoleAsync(user, "User");
                    break;
            }

            await _unitOfWork.CommitAsync();

            var createdUser = new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Id = user.Id
            };

            return CustomResponseDto<UserDto>.Success(200, createdUser);
        }

        public async Task<CustomResponseDto<UserDto>> EditAsync(UpdateUserDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(userDto.Id);

            if (user == null)
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<User>());
            }

            var result = await _userManager.UpdateAsync(user);

            if(!result.Succeeded)
            {
                throw new BadRequestException(MagicStrings.BadRequestDescription);
            }

            var userRole = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRoleAsync(user, userRole[0]);
            await _unitOfWork.CommitAsync();

            if (!await _roleManager.RoleExistsAsync(userDto.RoleName))
            {
                throw new NotFoundException(MagicStrings.RoleNotExist);  
            }

            switch (userDto.RoleName)
            {
                case "Admin":
                    await _userManager.AddToRolesAsync(user, new[] { "Admin", "Editor", "User" });
                    break;
                case "Editor":
                    await _userManager.AddToRolesAsync(user, new[] { "Editor", "User" });
                    break;
                case "User":
                    await _userManager.AddToRoleAsync(user, "User");
                    break;
                default:
                    await _userManager.AddToRoleAsync(user, userDto.RoleName);
                    await _userManager.AddToRoleAsync(user, "User");
                    break;
            }
            await _unitOfWork.CommitAsync();

            var updatedUser = new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Id = user.Id
            };

            return CustomResponseDto<UserDto>.Success(200, updatedUser);
        }

        public async Task<CustomResponseDto<UserByIdWithEntriesDto>> GetUserByIdWithEntries(int userId)
        {
            var user = await _userRepository.GetUserByIdWithEntries(userId);
            if (user == null)
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<User>());
            }
            var userDto = _mapper.Map<UserByIdWithEntriesDto>(user);
            return CustomResponseDto<UserByIdWithEntriesDto>.Success(200, userDto);
        }

        public async Task<CustomResponseDto<UserByIdWithTitlesDto>> GetUserByIdWithTitles(int userId)
        {
            var user = await _userRepository.GetUserByIdWithTitles(userId);
            if (user == null)
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<User>());
            }
            var userDto = _mapper.Map<UserByIdWithTitlesDto>(user);
            return CustomResponseDto<UserByIdWithTitlesDto>.Success(200, userDto);
        }

        public async Task<CustomResponseDto<List<UsersWithRoleDto>>> GetUsersWithRole()
        {
            var users = await _userRepository.GetUsersWithRole();
            if (users == null)
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<User>());
            }
            var usersDto = _mapper.Map<List<UsersWithRoleDto>>(users);
            return CustomResponseDto<List<UsersWithRoleDto>>.Success(200, usersDto);
        }
    }
}
