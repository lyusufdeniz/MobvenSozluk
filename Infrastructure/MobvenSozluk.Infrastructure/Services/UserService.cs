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

        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager) : base(repository, unitOfWork)
        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper, IPagingService<User> pagingService, ISortingService<User> sortingService, IFilteringService<User> filteringService) : base(repository, unitOfWork, sortingService, pagingService, mapper, filteringService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        #region CODE EXPLANATION SECTION 1
        /*
          Configure "UserManager" with "User" and "RoleManager" with "Role"
          CreateAsync and EditAsync methods are operates users with roles carefully.
               In this program "Admin" is a superRole, so it needs other roles automatically.
               In this code Roles with users have managed carefully.
          
         */
        #endregion


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
            #region CODE EXPLANATION SECTION 2
            /*
              Because of the role management. Program checks if created user`s roleName is "Admin" then it adds roles which are "Admin","Editor","User".
              If user`s role name is "Editor" then it needs both "Editor" and "User" role.
              If we create new role for instance "Helper" role. Then program understands it is a different role.
                   Then program adds that role with given user. 
                   But as you see "User" role is constant role then it means program adds "User" role with all specific roles automatically
             */
            #endregion

            await _unitOfWork.CommitAsync();

            var createdUser = new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Id = user.Id
            };

            #region CODE EXPLANATION SECTION 3
            /*
              It first checks if a user with the specified email already exists. 
                   If the user does not exist, it creates a new User object and assigns the values of the userDto object to its properties.
              It then checks if the role specified in userDto.RoleName exists in the system. 
                   If the role exists, it creates the user using the UserManager.CreateAsync method.
              Then, based on the role specified in userDto.RoleName, it adds the user to the appropriate roles using the UserManager.AddToRoleAsync method.
              Finally, it commits the changes to the database using a UnitOfWork object.
                      We use UnitOfWork pattern because we need to accumulate "SaveChangesAsync()" method in "Bussiness Layer".
                      Please check "MobvenSozluk.Repository.UnitOfWorks"

             */
            #endregion

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

            #region CODE EXPLANATION SECTION 4
            /*
              It first checks if the user exists by calling the _userRepository.GetByIdAsync method with the user ID. 
                     If the user exists, it updates the user's name and email using the data provided in userDto.
              It then calls the _userManager.UpdateAsync method to save the changes to the database. 
                    IMPORTANT!! = With Identity library functions, they automatically save the changes after it has used.
                                  But sometimes it works weirdly. To handle this we can use our Savechanges method with UnitOfWork
              Retrieves the current role of the user using the _userManager.GetRolesAsync method.
              Removes the user from their current role or roles using the _userManager.RemoveFromRoleAsync method.
              Checks if the new role provided in userDto exists using the _roleManager.RoleExistsAsync method.
              If the new role exists, it adds the user to that role using the _userManager.AddToRoleAsync method.
                 Please check "Code explanation section 3"
              It calls the _unitOfWork.CommitAsync method to save the changes to the database.       
             */
            #endregion

            var updatedUser = new UserDto
            {
                Username = userExists.UserName,
                Email = userExists.Email,
                Id = userExists.Id
            };

            return CustomResponseDto<UserDto>.Success(200, updatedUser);

            _pagingService = pagingService;
            _sortingService = sortingService;
            _filteringService = filteringService;
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
