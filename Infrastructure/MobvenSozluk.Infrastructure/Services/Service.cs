using AutoMapper;
using MobvenSozluk.Infrastructure.Exceptions;
using MobvenSozluk.Repository.DTOs.RequestDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Repositories;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Repository.UnitOfWorks;
using System.Linq.Expressions;

namespace MobvenSozluk.Infrastructure.Services
{
    public class Service<T, TDto> : IService<T, TDto> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISortingService<T> _sortingService;
        private readonly IPagingService<T> _pagingService;
        private readonly IMapper _mapper;
        private readonly IFilteringService<T> _filteringService;
        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork, ISortingService<T> sortingService, IPagingService<T> pagingService, IMapper mapper, IFilteringService<T> filteringService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _sortingService = sortingService;
            _pagingService = pagingService;
            _mapper = mapper;
            _filteringService = filteringService;
        }

        public async Task<CustomResponseDto<TDto>> AddAsync(TDto entity)
        {

            var mapped = _mapper.Map<T>(entity);
            await _repository.AddAsync(mapped);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<TDto>.Success(200, entity);

        }

        public async Task<CustomResponseDto<List<TDto>>> AddRangeAsync(List<TDto> entities)
        {
            var mapped = _mapper.Map<List<T>>(entities);
            await _repository.AddRangeAsync(mapped);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<List<TDto>>.Success(200, entities);
        }

        public async Task<CustomResponseDto<TDto>> AnyAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await _repository.AnyAsync(expression);

            if (entity == null)
            {
                throw new NotFoundException($"{typeof(T).Name} not found");
            }
            var mapped = _mapper.Map<TDto>(entity);
            return CustomResponseDto<TDto>.Success(200, mapped);

        }

        public async Task<CustomResponseDto<List<TDto>>> GetAllAsync(bool sortByDesc, string sortparameter, int pagenumber, int pageSize, List<FilterDTO> filters)
        {

            var entities = _repository.GetAll();

            if (entities == null)
            {
                throw new NotFoundException($"{typeof(T).Name} not found");
            }
            var filtereddata = _filteringService.GetFilteredData(entities, filters);
            var fitlerresult = _filteringService.FilterResult();
            var sorteddata = _sortingService.SortData(filtereddata, sortByDesc, sortparameter);
            var sortResult = _sortingService.SortingParameter();
            var finaldata = _pagingService.PageData(sorteddata, pagenumber, pageSize);
            var pageresult = _pagingService.PageResult();
            var mapped = _mapper.Map<List<TDto>>(finaldata);

            return CustomResponseDto<List<TDto>>.Success(200, mapped, pageresult, sortResult, fitlerresult);
        }

        public async Task<CustomResponseDto<TDto>> RemoveAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            var mapped = _mapper.Map<TDto>(entity);
            return CustomResponseDto<TDto>.Success(204);
        }

        public async Task<CustomResponseDto<List<TDto>>> RemoveRangeAsync(List<TDto> entities)
        {
            var mapped = _mapper.Map<IEnumerable<T>>(entities);
            _repository.RemoveRange(mapped);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<List<TDto>>.Success(204, entities);
        }

        public async Task<CustomResponseDto<TDto>> UpdateAsync(TDto entity)
        {
            var mapped = _mapper.Map<T>(entity);
            _repository.Update(mapped);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<TDto>.Success(204);
        }

        public async Task<CustomResponseDto<List<TDto>>> Where(Expression<Func<T, bool>> expression)
        {
            var entities = _repository.Where(expression);

            if (entities == null)
            {
                throw new NotFoundException($"{typeof(T).Name} not found");
            }
            var mapped = _mapper.Map<List<TDto>>(entities);
            return CustomResponseDto<List<TDto>>.Success(200, mapped);

        }

        public async Task<CustomResponseDto<TDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"{typeof(T).Name}({id}) not found");
            }
            var mapped = _mapper.Map<TDto>(entity);

            return CustomResponseDto<TDto>.Success(200, mapped);

        }
    }
}