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
    public class CategoryService : Service<Category,CategoryDto>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IPagingService<Category> _pagingService;
        private readonly ISortingService<Category> _sortingService;
        private readonly IFilteringService<Category> _filteringService;
        private readonly ISearchingService<Category> _searchingService;
        private readonly ICacheService<CategoryDto> _cacheService;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper, IPagingService<Category> pagingService, ISortingService<Category> sortingService, IFilteringService<Category> filteringService, ISearchingService<Category> searchingService, ICacheService<CategoryDto> cacheService) : base(repository, unitOfWork, sortingService, pagingService, mapper, filteringService,searchingService)
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
        
        public override async Task<CustomResponseDto<List<CategoryDto>>> GetAllAsync(bool sortByDesc, string sortparameter, int pagenumber, int pageSize, List<FilterDTO> filters)
        {
            var cacheKey = "Categories";
            var cachedData = _cacheService.Get<CustomResponseDto<List<CategoryDto>>>(cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            
            var entities = _categoryRepository.GetAll().ToList();
            if (entities == null)
            {
                throw new NotFoundException($"{typeof(Category).Name} not found");
            }
            
            var filtereddata = _filteringService.GetFilteredData(entities, filters);
            var filterresult = _filteringService.FilterResult();
            var sorteddata = _sortingService.SortData(filtereddata, sortByDesc, sortparameter);
            var sortResult = _sortingService.SortResult();
            var finaldata = _pagingService.PageData(sorteddata, pagenumber, pageSize);
            var pageresult = _pagingService.PageResult();
            var mapped = _mapper.Map<List<CategoryDto>>(finaldata);
            
            var response = CustomResponseDto<List<CategoryDto>>.Success(200, mapped, pageresult, sortResult, filterresult);
            _cacheService.Set(cacheKey, response, DateTimeOffset.UtcNow.AddMinutes(3));
            return response;
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


