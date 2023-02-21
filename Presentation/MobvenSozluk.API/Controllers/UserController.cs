using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs.CustomResponse;
using MobvenSozluk.Repository.DTOs;
using MobvenSozluk.Repository.Services;

namespace MobvenSozluk.API.Controllers
{
    
    public class UserController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<User> _service;

        public UserController(IMapper mapper, IService<User> service)
        {
            _mapper = mapper;
            _service = service;
        }

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
