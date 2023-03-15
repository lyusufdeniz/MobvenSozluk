using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using Microsoft.AspNetCore.Authorization;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.RequestDTOs;
using MobvenSozluk.Repository.Services;

namespace MobvenSozluk.API.Controllers
{
    
    public class RoleController : CustomBaseController
    {

        private readonly IRoleService _service;

        public RoleController(IRoleService roleService)
        {
            _service = roleService;
        }


        [HttpGet("[action]/{roleId}")]
        public async Task<IActionResult> GetRoleByIdWithUsers(int roleId)
        {
            return CreateActionResult(await _service.GetRoleByIdWithUsers(roleId));
        }


        [HttpPost]
        public async Task<IActionResult> All(int pageNo, int pageSize, bool sortByDesc, string sortParameter, List<FilterDTO>? Filters)
        {

            return CreateActionResult(await _service.GetAllAsync(sortByDesc, sortParameter, pageNo, pageSize, Filters));


        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _service.GetByIdAsync(id));
        }


        [HttpPost("{action}")]
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
            return CreateActionResult(await _service.RemoveAsync(id));
        }
    }
}
