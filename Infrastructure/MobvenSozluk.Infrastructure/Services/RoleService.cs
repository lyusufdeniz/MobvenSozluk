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
    public class RoleService : Service<Role,RoleDto>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public RoleService(IGenericRepository<Role> repository, IUnitOfWork unitOfWork, IRoleRepository roleRepository, IMapper mapper, RoleManager<Role> roleManager, UserManager<User> userManager) : base(repository, unitOfWork)
        private readonly IPagingService<Role> _pagingService;
        private readonly ISortingService<Role> _sortingService;
        private readonly IFilteringService<Role> _filteringService;

        public RoleService(IGenericRepository<Role> repository, IUnitOfWork unitOfWork, IRoleRepository roleRepository, IMapper mapper, IPagingService<Role> pagingService, ISortingService<Role> sortingService, IFilteringService<Role> filteringService) : base(repository, unitOfWork, sortingService, pagingService, mapper,filteringService)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        #region CODE EXPLANATION SECTION 1
        /*
          Configure "UserManager" with "User" and "RoleManager" with "Role"
          CreateAsync and EditAsync methods are operates roles with users carefully.
               In this program "Admin" is a superRole, so it needs other roles automatically.
               Program configures when any role added to the program; 
               It is automatically finds users who is "Admin" and includes the new role into "Admin" initially
          
         */
        #endregion

        public async Task<CustomResponseDto<RoleDto>> CreateAsync(AddRoleDto roleDto)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleDto.Name);

            if (roleExists)
            {
                throw new NotFoundException($"{typeof(Role).Name} already exist");
            }

            var role = new Role
            {
                Name = roleDto.Name
            };

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                throw new NotFoundException($"Something went wrong");
            }

            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");

            foreach(var user in adminUsers)
            {
                await _userManager.AddToRoleAsync(user, role.Name);
            }

            var createdRole = new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };

            #region CODE EXPLANATION SECTION 2
            /*
              It first checks if the role already exists in the system by calling the _roleManager.
                 RoleExistsAsync method with the name of the role provided in roleDto
                 If the role does not exist, it creates a new Role object with the name provided in roleDto.
              It then calls the _roleManager.CreateAsync method to save the new role to the database.
              After the new role is created,
                   it adds the new role to all users with the "Admin" role using the _userManager.GetUsersInRoleAsync method to 
                   retrieve all users in the "Admin" role, and then adding the new role to each of them using the _userManager.AddToRoleAsync method.

             */
            #endregion

            return CustomResponseDto<RoleDto>.Success(200, createdRole);
            _pagingService = pagingService;
            _sortingService = sortingService;
            _filteringService = filteringService;
        }

        public async Task<CustomResponseDto<RoleDto>> EditAsync(RoleDto roleDto)
        {
            var databaseRole = await _roleManager.FindByIdAsync(roleDto.Id.ToString());

            if (databaseRole == null)
            {
                throw new NotFoundException($"{typeof(Role).Name} not found");
            }

            databaseRole.Name = roleDto.Name;

            await _roleManager.UpdateAsync(databaseRole);

            #region CODE EXPLANATION SECTION 3
            /*
              It first retrieves the existing role from the database using the _roleManager.FindByIdAsync method with the role ID provided in roleDto.
                     If the role exists, it updates the role's name to the name provided in roleDto.
              It then calls the _roleManager.UpdateAsync method to save the changes to the database.
             */
            #endregion

            return CustomResponseDto<RoleDto>.Success(200, roleDto);

        }

        public async Task<CustomResponseDto<RoleByIdWithUsersDto>> GetRoleByIdWithUsers(int roleId)
        {
           
            var role = await _roleRepository.GetRoleByIdWithUsers(roleId);
     
            if(role== null)
            {
                throw new NotFoundException($"{typeof(Role).Name} not found");
            }

            var roleDto = _mapper.Map<RoleByIdWithUsersDto>(role);

            return CustomResponseDto<RoleByIdWithUsersDto>.Success(200, roleDto);
        }
    }
}
