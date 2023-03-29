using AutoMapper;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Infrastructure.Exceptions;
using MobvenSozluk.Repository.Cache;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.RequestDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Repositories;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Repository.UnitOfWorks;

namespace MobvenSozluk.Infrastructure.Services
{
    public class CategoryService : Service<Category, CategoryDto>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IPagingService<Category> _pagingService;
        private readonly ISortingService<Category> _sortingService;
        private readonly IFilteringService<Category> _filteringService;
        private readonly ISearchingService<Category> _searchingService;
        private readonly ICacheService<CategoryDto> _cacheService;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper, IPagingService<Category> pagingService, ISortingService<Category> sortingService, IFilteringService<Category> filteringService, ISearchingService<Category> searchingService, ICacheService<CategoryDto> cacheService) : base(repository, unitOfWork, sortingService, pagingService, mapper, filteringService, searchingService)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _pagingService = pagingService;
            _sortingService = sortingService;
            _filteringService = filteringService;
            _searchingService = searchingService;
            _cacheService = cacheService;
        }

        public async override Task<CustomResponseDto<CategoryDto>> AddAsync(CategoryDto entity)
        {
            var cacheKey = "Categories";
            if (_cacheService.Exists(cacheKey))
            {
                _cacheService.Remove(cacheKey);
            }

            var mapped = _mapper.Map<Category>(entity);
            await _categoryRepository.AddAsync(mapped);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<CategoryDto>.Success(200, entity);
        }

        public override async Task<CustomResponseDto<List<CategoryDto>>> GetAllAsync(bool sortByDesc, string sortparameter, int pagenumber, int pageSize, List<FilterDTO> filters)
        {
            var cacheKey = "Categories";
            List<CategoryDto> categoryDtos;

            if (_cacheService.Exists(cacheKey))
            {
                categoryDtos = _cacheService.Get<List<CategoryDto>>(cacheKey);
                return CustomResponseDto<List<CategoryDto>>.Success(200, categoryDtos);
            }

            var categories = _categoryRepository.GetAll().ToList();
            categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            _cacheService.Set(cacheKey, categoryDtos, DateTimeOffset.UtcNow.AddMinutes(3));

            var filtereddata = _filteringService.GetFilteredData(categories, filters, out FilterResult filterResult);
            var sorteddata = _sortingService.SortData(filtereddata, sortByDesc, sortparameter, out SortingResult sortingResult);
            var finaldata = _pagingService.PageData(sorteddata, pagenumber, pageSize, out PagingResult pagingResult);
            var mapped = _mapper.Map<List<CategoryDto>>(finaldata);

            return CustomResponseDto<List<CategoryDto>>.Success(200, mapped, pagingResult, sortingResult, filterResult);
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

        public async override Task<CustomResponseDto<CategoryDto>> RemoveAsync(int id)
        {
            var cacheKey = $"Categories";
            var item = await _categoryRepository.GetByIdAsync(id);

            if (item == null)
            {
                throw new NotFoundException($"{typeof(Category).Name}({id}) not found");
            }
            if (_cacheService.Exists(cacheKey))
            {
                _cacheService.Remove(cacheKey);

            }
            _categoryRepository.Remove(item);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<CategoryDto>.Success(204);
        }

        public async override Task<CustomResponseDto<CategoryDto>> UpdateAsync(CategoryDto entity)
        {
            var cacheKey = $"Categories";
            try
            {
                var mapped = _mapper.Map<Category>(entity);
                _categoryRepository.Update(mapped);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new NotFoundException($"{typeof(Category).Name} not found");
            }
            if (_cacheService.Exists(cacheKey))
            {
                _cacheService.Remove(cacheKey);

            }
            return CustomResponseDto<CategoryDto>.Success(204);
        }
    }

}


