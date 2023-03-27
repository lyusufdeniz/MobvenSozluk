using AutoMapper;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Infrastructure.Exceptions;
using MobvenSozluk.Repository.Cache;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Repositories;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Repository.UnitOfWorks;

namespace MobvenSozluk.Infrastructure.Services
{
    public class CategoryService : Service<Category,CategoryDto>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        private readonly ICacheService<CategoryDto> _cacheService;
        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper, IPagingService<Category> pagingService, ISortingService<Category> sortingService, IFilteringService<Category> filteringService, ISearchingService<Category> searchingService, ICacheService<CategoryDto> cacheService) : base(repository, unitOfWork, sortingService, pagingService, mapper, filteringService,searchingService)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;

            _cacheService = cacheService;
        }

        public async Task<CustomResponseDto<CategoryByIdWithTitlesDto>> GetCategoryByIdWithTitles(int categoryId)
        {
            var cacheKey = $"category_{categoryId}";
            var cachedValue = _cacheService.Get<CategoryByIdWithTitlesDto>(cacheKey);
            if (cachedValue == null)
            {
                var category = await _categoryRepository.GetCategoryByIdWithTitles(categoryId);
                if (category == null)
                {
                    throw new NotFoundException($"{typeof(Category).Name} not found");
                }
                cachedValue = _mapper.Map<CategoryByIdWithTitlesDto>(category);

                _cacheService.Set(cacheKey, cachedValue, DateTimeOffset.UtcNow.AddMinutes(3));
            }
            return CustomResponseDto<CategoryByIdWithTitlesDto>.Success(200, cachedValue);
        }

    }
}

