using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.Repository.DTOs.CustomResponse;
using MobvenSozluk.Repository.DTOs;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Domain.Concrete.Entities;

namespace MobvenSozluk.API.Controllers
{
    public class RoleController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Role> _service;

        public RoleController(IMapper mapper, IService<Role> service)
        {
            _mapper = mapper;
            _service = service;
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
        public async Task<IActionResult> Save(RoleDto roleDto)
        {
            var role = await _service.AddAsync(_mapper.Map<Role>(roleDto));

            var rolesDto = _mapper.Map<RoleDto>(role);

            return CreateActionResult(CustomResponseDto<RoleDto>.Success(200, rolesDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleDto roleDto)
        {
            await _service.UpdateAsync(_mapper.Map<Role>(roleDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
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
