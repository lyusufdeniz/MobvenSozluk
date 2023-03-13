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
    public class RoleService : Service<Role,RoleDto>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IPagingService<Role> _pagingService;
        private readonly ISortingService<Role> _sortingService;
        private readonly IFilteringService<Role> _filteringService;

        public RoleService(IGenericRepository<Role> repository, IUnitOfWork unitOfWork, IRoleRepository roleRepository, IMapper mapper, IPagingService<Role> pagingService, ISortingService<Role> sortingService, IFilteringService<Role> filteringService) : base(repository, unitOfWork, sortingService, pagingService, mapper,filteringService)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _pagingService = pagingService;
            _sortingService = sortingService;
            _filteringService = filteringService;
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
