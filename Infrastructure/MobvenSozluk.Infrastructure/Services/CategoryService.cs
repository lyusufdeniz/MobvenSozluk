using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Infrastructure.Exceptions;
using MobvenSozluk.Persistance.UnitOfWorks;
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
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICacheService<Category> _cacheService;
        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper, IPagingService<Category> pagingService, ISortingService<Category> sortingService, IFilteringService<Category> filteringService, ISearchingService<Category> searchingService, ICacheService<Category> cacheService) : base(repository, unitOfWork, sortingService, pagingService, mapper, filteringService, searchingService)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _pagingService = pagingService;
            _sortingService = sortingService;
            _filteringService = filteringService;
            _searchingService = searchingService;
            _cacheService = cacheService;
            _unitOfWork=unitOfWork;
        }

        public override async Task<CustomResponseDto<List<CategoryDto>>> GetAllAsync(bool sortByDesc, string sortparameter, int pagenumber, int pageSize, List<FilterDTO> filters)
        {
            var cacheKey = $"Categories";
            List<Category> data;

            if (_cacheService.Exists(cacheKey))
            {
                data = _cacheService.GetAll(cacheKey);

            }
            else
            {
                data = _categoryRepository.GetAll().ToList();
                _cacheService.Set(cacheKey, data, DateTimeOffset.UtcNow.AddMinutes(3));

            }

            var filtereddata = _filteringService.GetFilteredData(data, filters, out FilterResult filterResult);
            var sorteddata = _sortingService.SortData(filtereddata, sortByDesc, sortparameter, out SortingResult sortingResult);
            var finaldata = _pagingService.PageData(sorteddata, pagenumber, pageSize,out PagingResult pagingResult);
            var mapped = _mapper.Map<List<CategoryDto>>(finaldata);

            return CustomResponseDto<List<CategoryDto>>.Success(200, mapped, pagingResult, sortingResult, filterResult);
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

