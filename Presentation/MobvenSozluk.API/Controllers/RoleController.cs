using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Infrastructure.Services;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using Microsoft.AspNetCore.Authorization;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;

namespace MobvenSozluk.API.Controllers
{
    
    public class RoleController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IRoleService _service;

        public RoleController(IMapper mapper, IRoleService roleService)
        {
            _mapper = mapper;
            _service = roleService;
        }


        [HttpGet("[action]/{roleId}")]
        public async Task<IActionResult> GetRoleByIdWithUsers(int roleId)
        {
            return CreateActionResult(await _service.GetRoleByIdWithUsers(roleId));
        }

    
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var roles = await _service.GetAllAsync();

            var rolesDtos = _mapper.Map<List<RoleDto>>(roles.ToList());

            return CreateActionResult(CustomResponseDto<List<RoleDto>>.Success(200, rolesDtos));
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _service.GetByIdAsync(id);

            var rolesDto = _mapper.Map<RoleDto>(role);

            return CreateActionResult(CustomResponseDto<RoleDto>.Success(200, rolesDto));
        }


        [HttpPost]
        public async Task<IActionResult> Save(AddRoleDto roleDto)
        {
           
            return CreateActionResult(await _service.CreateAsync(roleDto));
        }

        
        [HttpPut]
        public async Task<IActionResult> Update(RoleDto roleDto)
        {
            return CreateActionResult(await _service.EditAsync(roleDto));
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var role = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(role);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
