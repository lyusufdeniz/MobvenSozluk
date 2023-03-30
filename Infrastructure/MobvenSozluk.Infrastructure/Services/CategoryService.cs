using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Domain.Constants;
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
        private readonly ICacheService<Category> _cacheService;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper, IPagingService<Category> pagingService, ISortingService<Category> sortingService, IFilteringService<Category> filteringService, ISearchingService<Category> searchingService, ICacheService<Category> cacheService) : base(repository, unitOfWork, sortingService, pagingService, mapper, filteringService, searchingService)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _pagingService = pagingService;
            _sortingService = sortingService;
            _filteringService = filteringService;
            _cacheService = cacheService;
        }

        public async override Task<CustomResponseDto<CategoryDto>> AddAsync(CategoryDto entity)
        {
            if (_cacheService.Exists(MagicStrings.CategoriesCacheKey))
            {
                _cacheService.Remove(MagicStrings.CategoriesCacheKey);
            }

            var mapped = _mapper.Map<Category>(entity);
            await _categoryRepository.AddAsync(mapped);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<CategoryDto>.Success(200, entity);
        }

        public override async Task<CustomResponseDto<List<CategoryDto>>> GetAllAsync(bool sortByDesc, string sortparameter, int pagenumber, int pageSize, List<FilterDTO> filters)
        {

            List<Category> categories;

            if (!_cacheService.Exists(MagicStrings.CategoriesCacheKey))
            {

                categories = await _categoryRepository.GetAll().ToListAsync();
                var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
                _cacheService.Set(MagicStrings.CategoriesCacheKey, categoryDtos, DateTimeOffset.UtcNow.AddMinutes(3));
            }

            categories = _cacheService.Get<List<Category>>(MagicStrings.CategoriesCacheKey);


            var filtereddata = _filteringService.GetFilteredData(categories, filters, out FilterResult filterResult);
            var sorteddata = _sortingService.SortData(filtereddata, sortByDesc, sortparameter, out SortingResult sortingResult);
            var finaldata = _pagingService.PageData(sorteddata, pagenumber, pageSize, out PagingResult pagingResult);
            var mapped = _mapper.Map<List<CategoryDto>>(finaldata);

            return CustomResponseDto<List<CategoryDto>>.Success(200, mapped, pagingResult, sortingResult, filterResult);
        }


        public async Task<CustomResponseDto<CategoryByIdWithTitlesDto>> GetCategoryByIdWithTitles(int categoryId)
        {
            var cacheKey = MagicStrings.CategoryCacheKey(categoryId);
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

            var item = await _categoryRepository.GetByIdAsync(id);

            if (item == null)
            {
                throw new NotFoundException($"{typeof(Category).Name}({id}) not found");
            }
            if (_cacheService.Exists(MagicStrings.CategoriesCacheKey))
            {
                _cacheService.Remove(MagicStrings.CategoriesCacheKey);

            }
            _categoryRepository.Remove(item);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<CategoryDto>.Success(204);
        }

        public async override Task<CustomResponseDto<CategoryDto>> UpdateAsync(CategoryDto entity)
        {

            try
            {
                var mapped = _mapper.Map<Category>(entity);
                _categoryRepository.Update(mapped);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<Category>());
            }
            if (_cacheService.Exists(MagicStrings.CategoriesCacheKey))
            {
                _cacheService.Remove(MagicStrings.CategoriesCacheKey);

            }
            return CustomResponseDto<CategoryDto>.Success(204);
        }
    }

}


