using AutoMapper;
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
    public class UserService : Service<User,UserDto>, IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPagingService<User> _pagingService;
        private readonly ISortingService<User> _sortingService;

        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper, IPagingService<User> pagingService, ISortingService<User> sortingService) : base(repository, unitOfWork,sortingService,pagingService,mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _pagingService = pagingService;
            _sortingService = sortingService;
        }

        public async Task<CustomResponseDto<UserByIdWithEntriesDto>> GetUserByIdWithEntries(int userId)
        {
            var user = await _userRepository.GetUserByIdWithEntries(userId);
            if (user == null)
            {
                throw new NotFoundException($"{typeof(User).Name} not found");
            }
            var userDto = _mapper.Map<UserByIdWithEntriesDto>(user);
            return CustomResponseDto<UserByIdWithEntriesDto>.Success(200, userDto);
        }

        public async Task<CustomResponseDto<UserByIdWithTitlesDto>> GetUserByIdWithTitles(int userId)
        {
            var user = await _userRepository.GetUserByIdWithTitles(userId);
            if (user == null)
            {
                throw new NotFoundException($"{typeof(User).Name} not found");
            }
            var userDto = _mapper.Map<UserByIdWithTitlesDto>(user);
            return CustomResponseDto<UserByIdWithTitlesDto>.Success(200, userDto);
        }

        public async Task<CustomResponseDto<List<UsersWithRoleDto>>> GetUsersWithRole()
        {
            var users = await _userRepository.GetUsersWithRole();
            if (users == null)
            {
                throw new NotFoundException($"{typeof(User).Name} not found");
            }
            var usersDto = _mapper.Map<List<UsersWithRoleDto>>(users);
            return CustomResponseDto<List<UsersWithRoleDto>>.Success(200, usersDto);
        }
    }
}
