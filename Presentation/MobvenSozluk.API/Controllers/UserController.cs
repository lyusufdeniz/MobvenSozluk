using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;

namespace MobvenSozluk.API.Controllers
{
    
    public class UserController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _service;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
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
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var users = await _service.GetAllAsync();

            var userDtos = _mapper.Map<List<UserDto>>(users.ToList());

            return CreateActionResult(CustomResponseDto<List<UserDto>>.Success(200, userDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetByIdAsync(id);

            var usersDto = _mapper.Map<UserDto>(user);

            return CreateActionResult(CustomResponseDto<UserDto>.Success(200, usersDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserDto userDto)
        {
            var user = await _service.AddAsync(_mapper.Map<User>(userDto));

            var usersDto = _mapper.Map<UserDto>(user);

            return CreateActionResult(CustomResponseDto<UserDto>.Success(200, usersDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserDto userDto)
        {
            await _service.UpdateAsync(_mapper.Map<User>(userDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var user = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(user);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
