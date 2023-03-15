using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.RequestDTOs;
using MobvenSozluk.Repository.Services;

namespace MobvenSozluk.API.Controllers
{
    
    public class UserController : CustomBaseController
    {

        private readonly IUserService _service;

        public UserController(IUserService userService)
        {

            _service = userService;
        }

        //Get api/users/GetUsersWithRole
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUsersWithRole()
        {

            return CreateActionResult(await _service.GetUsersWithRole());
        }


        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetUserByIdWithEntries(int userId)
        {
            return CreateActionResult(await _service.GetUserByIdWithEntries(userId));
        }

        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetUserByIdWithTitles(int userId)
        {
            return CreateActionResult(await _service.GetUserByIdWithTitles(userId));
        }

        //Get api/users
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
        public async Task<IActionResult> Save(UserDto user)
        {
            return CreateActionResult(await _service.AddAsync(user));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDto userDto)
        {
            return CreateActionResult(await _service.EditAsync(userDto));
           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }
    }
}
