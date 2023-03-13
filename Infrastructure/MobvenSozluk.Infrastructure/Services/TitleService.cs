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
    public class TitleService : Service<Title,TitleDto>, ITitleService
    {
        private readonly ITitleRepository _titleRepository;
        private readonly IMapper _mapper;
        private readonly IPagingService<Title> _pagingService;
        private readonly ISortingService<Title> _sortingService;
        private readonly IFilteringService<Title> _filteringService;
        public TitleService(IGenericRepository<Title> repository, IUnitOfWork unitOfWork, ITitleRepository titleRepository, IMapper mapper, IPagingService<Title> pagingService, ISortingService<Title> sortingService, IFilteringService<Title> filteringService) : base(repository, unitOfWork, sortingService, pagingService, mapper,filteringService)
        {
            _titleRepository = titleRepository;
            _mapper = mapper;
            _pagingService = pagingService;
            _sortingService = sortingService;
            _filteringService = filteringService;
        }

        public async Task<CustomResponseDto<TitleByIdWithEntriesDto>> GetTitleByIdWithEntries(int titleId)
        {
            var title = await _titleRepository.GetTitleByIdWithEntries(titleId);
            if (title == null)
            {
                throw new NotFoundException($"{typeof(Entry).Name} not found");
            }
            var titleDto = _mapper.Map<TitleByIdWithEntriesDto>(title);
            return CustomResponseDto<TitleByIdWithEntriesDto>.Success(200, titleDto);
        }

        public async Task<CustomResponseDto<List<TitlesWithUserAndCategoryDto>>> GetTitlesWithUserAndCategory()
        {
            var titles = await _titleRepository.GetTitlesWithUserAndCategory();
            var titlesDto = _mapper.Map<List<TitlesWithUserAndCategoryDto>>(titles);
            return CustomResponseDto<List<TitlesWithUserAndCategoryDto>>.Success(200, titlesDto);
        }
    }
}
