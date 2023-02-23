using AutoMapper;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Persistance.Repositories;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Repositories;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Repository.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Infrastructure.Services
{
    public class UserService : Service<User>, IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<UserByIdWithEntriesDto>> GetUserByIdWithEntries(int userId)
        {
            var user = await _userRepository.GetUserByIdWithEntries(userId);
            var userDto = _mapper.Map<UserByIdWithEntriesDto>(user);
            return CustomResponseDto<UserByIdWithEntriesDto>.Success(200, userDto);
        }

        public async Task<CustomResponseDto<UserByIdWithTitlesDto>> GetUserByIdWithTitles(int userId)
        {
            var user = await _userRepository.GetUserByIdWithTitles(userId);
            var userDto = _mapper.Map<UserByIdWithTitlesDto>(user);
            return CustomResponseDto<UserByIdWithTitlesDto>.Success(200, userDto);
        }

        public async Task<CustomResponseDto<List<UsersWithRoleDto>>> GetUsersWithRole()
        {
            var users = await _userRepository.GetUsersWithRole();
            var usersDto = _mapper.Map<List<UsersWithRoleDto>>(users);
            return CustomResponseDto<List<UsersWithRoleDto>>.Success(200, usersDto);
        }
    }
}
