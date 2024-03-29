﻿using MobvenSozluk.Domain.Concrete.Entities;
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
    public interface IRoleService: IService<Role,RoleDto>
    {
        Task<CustomResponseDto<RoleByIdWithUsersDto>> GetRoleByIdWithUsers(int roleId);
        Task<CustomResponseDto<RoleDto>> CreateAsync(AddRoleDto roleDto);
        Task<CustomResponseDto<RoleDto>> EditAsync(RoleDto roleDto);
    }
}
