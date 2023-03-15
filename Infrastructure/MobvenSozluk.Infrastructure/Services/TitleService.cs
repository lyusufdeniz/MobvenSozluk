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
        private readonly ISearchingService<Title> _searchingService;
        public TitleService(IGenericRepository<Title> repository, IUnitOfWork unitOfWork, ITitleRepository titleRepository, IMapper mapper, IPagingService<Title> pagingService, ISortingService<Title> sortingService, IFilteringService<Title> filteringService, ISearchingService<Title> searchingService) : base(repository, unitOfWork, sortingService, pagingService, mapper, filteringService, searchingService)
        {
            _titleRepository = titleRepository;
            _mapper = mapper;
            _pagingService = pagingService;
            _sortingService = sortingService;
            _filteringService = filteringService;
            _searchingService = searchingService;
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

        public async Task<List<TitleDto>> GetPopularTitlesAsync()
        {
            var popularTitles = await _titleRepository.GetPopularTitlesWithEntries();
            var popularTitlesDto = _mapper.Map<List<TitleDto>>(popularTitles.Take(20)); 
            return popularTitlesDto;
            
        }

        public async Task<CustomResponseDto<List<TitlesWithUserAndCategoryDto>>> GetTitlesWithUserAndCategory()
        {
            var titles = await _titleRepository.GetTitlesWithUserAndCategory();
            var titlesDto = _mapper.Map<List<TitlesWithUserAndCategoryDto>>(titles);
            return CustomResponseDto<List<TitlesWithUserAndCategoryDto>>.Success(200, titlesDto);
        }
    }
}
