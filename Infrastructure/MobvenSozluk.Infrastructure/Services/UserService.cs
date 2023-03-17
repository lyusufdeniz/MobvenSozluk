using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MobvenSozluk.Domain.Concrete.Entities;
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
        private readonly IPagingService<User> _pagingService;
        private readonly ISortingService<User> _sortingService;
        private readonly IFilteringService<User> _filteringService;


        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper, IPagingService<User> pagingService, ISortingService<User> sortingService, IFilteringService<User> filteringService, UserManager<User> userManager, RoleManager<Role> roleManager) : base(repository, unitOfWork, sortingService, pagingService, mapper, filteringService)
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
                throw new NotFoundException($"{typeof(User).Name} already exist");
            }

            var user = new User
            {
                UserName = userDto.Name,
                Email = userDto.Email,
            };


            if(!await _roleManager.RoleExistsAsync(userDto.RoleName))
            {
                throw new NotFoundException("There is no such role name exist");
            }
            else
            {
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    throw new NotFoundException($"Something went wrong");
                }
            }

            if (userDto.RoleName == "Admin")
            {
                await _userManager.AddToRolesAsync(user, new[] { "Admin", "Editor", "User" });
            }
            else if (userDto.RoleName == "Editor")
            {
                await _userManager.AddToRolesAsync(user, new[] { "Editor", "User" });
            }
            else if (userDto.RoleName == "User")
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, userDto.RoleName);
                await _userManager.AddToRoleAsync(user, "User");
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
            var userExists = await _userRepository.GetByIdAsync(userDto.Id);

            if (userExists == null)
            {
                throw new NotFoundException($"{typeof(User).Name} not found");
            }

            userExists.UserName = userDto.Name;
            userExists.Email = userDto.Email;

            var result = await _userManager.UpdateAsync(userExists);

            if(!result.Succeeded)
            {
                throw new NotFoundException("An error Accured");
            }

            var userRole = await _userManager.GetRolesAsync(userExists);

            await _userManager.RemoveFromRoleAsync(userExists, userRole[0]);
            await _unitOfWork.CommitAsync();
            if (await _roleManager.RoleExistsAsync(userDto.RoleName))
            {
                if (userDto.RoleName == "Admin")
                {
                    await _userManager.AddToRolesAsync(userExists, new[] { "Admin", "Editor", "User" });
                }
                else if (userDto.RoleName == "Editor")
                {
                    await _userManager.AddToRolesAsync(userExists, new[] { "Editor", "User" });
                }
                else if (userDto.RoleName == "User")
                {
                    await _userManager.AddToRoleAsync(userExists, "User");
                }
                else
                {
                    await _userManager.AddToRoleAsync(userExists, userDto.RoleName);
                    await _userManager.AddToRoleAsync(userExists, "User");
                } 
            }
            else
            {
                throw new NotFoundException("There is no such role name exist");
            }

            await _unitOfWork.CommitAsync();

            var updatedUser = new UserDto
            {
                Username = userExists.UserName,
                Email = userExists.Email,
                Id = userExists.Id
            };

            return CustomResponseDto<UserDto>.Success(200, updatedUser);

          
        }

        public async Task<CustomResponseDto<UserByIdWithEntriesDto>> GetUserByIdWithEntries(int userId)
        {
            var user = await _userRepository.GetUserByIdWithEntries(userId);
            if (user == null)
            {
                throw new NotFoundException($"{typeof(User).Name} not found");
            }
            var userDto = _mapper.Map<UserByIdWithEntriesDto>(user);
            return CustomResponseDto<UserByIdWithEntriesDto>.Success(200, userDto);
        }

        public async Task<CustomResponseDto<UserByIdWithTitlesDto>> GetUserByIdWithTitles(int userId)
        {
            var user = await _userRepository.GetUserByIdWithTitles(userId);
            if (user == null)
            {
                throw new NotFoundException($"{typeof(User).Name} not found");
            }
            var userDto = _mapper.Map<UserByIdWithTitlesDto>(user);
            return CustomResponseDto<UserByIdWithTitlesDto>.Success(200, userDto);
        }

        public async Task<CustomResponseDto<List<UsersWithRoleDto>>> GetUsersWithRole()
        {
            var users = await _userRepository.GetUsersWithRole();
            if (users == null)
            {
                throw new NotFoundException($"{typeof(User).Name} not found");
            }
            var usersDto = _mapper.Map<List<UsersWithRoleDto>>(users);
            return CustomResponseDto<List<UsersWithRoleDto>>.Success(200, usersDto);
        }
    }
}
