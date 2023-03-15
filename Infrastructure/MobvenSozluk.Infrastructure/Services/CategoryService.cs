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
    public class CategoryService : Service<Category,CategoryDto>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IPagingService<Category> _pagingService;
        private readonly ISortingService<Category> _sortingService;
        private readonly IFilteringService<Category> _filteringService;
        private readonly ISearchingService<Category> _searchingService;
        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper, IPagingService<Category> pagingService, ISortingService<Category> sortingService, IFilteringService<Category> filteringService, ISearchingService<Category> searchingService) : base(repository, unitOfWork, sortingService, pagingService, mapper, filteringService,searchingService)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _pagingService = pagingService;
            _sortingService = sortingService;
            _filteringService = filteringService;
            _searchingService = searchingService;
        }

        public async Task<CustomResponseDto<CategoryByIdWithTitlesDto>> GetCategoryByIdWithTitles(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdWithTitles(categoryId);
            if (category == null)
            {
                throw new NotFoundException($"{typeof(Category).Name} not found");
            }
            var categoryDto = _mapper.Map<CategoryByIdWithTitlesDto>(category);
            return CustomResponseDto<CategoryByIdWithTitlesDto>.Success(200, categoryDto);
        }
    }
}
