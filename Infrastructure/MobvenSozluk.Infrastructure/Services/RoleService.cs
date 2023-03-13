using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Infrastructure.Exceptions;
using MobvenSozluk.Persistance.Repositories;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Repositories;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Repository.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Infrastructure.Services
{
    public class RoleService : Service<Role>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public RoleService(IGenericRepository<Role> repository, IUnitOfWork unitOfWork, IRoleRepository roleRepository, IMapper mapper, RoleManager<Role> roleManager, UserManager<User> userManager) : base(repository, unitOfWork)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }

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

            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");// In here we are configure the super role

            foreach(var user in adminUsers)
            {
                await _userManager.AddToRoleAsync(user, role.Name);
            }
            //which is admin role; We are assigning created new roles initially into admin because admin is a super role.

            var createdRole = new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };

            return CustomResponseDto<RoleDto>.Success(200, createdRole);
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
