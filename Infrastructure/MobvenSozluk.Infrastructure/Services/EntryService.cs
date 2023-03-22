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
    public class EntryService : Service<Entry,EntryDto>, IEntryService
    {
        private readonly IEntryRepository _entryRepository;
        private readonly IMapper _mapper;
        private readonly IPagingService<Entry> _pagingService;
        private readonly ISortingService<Entry> _sortingService;
        private readonly IFilteringService<Entry> _filteringService;
        private readonly ISearchingService<Entry> _searchingService;
        public EntryService(IGenericRepository<Entry> repository, IUnitOfWork unitOfWork, IEntryRepository entryRepository, IMapper mapper, IPagingService<Entry> pagingService, ISortingService<Entry> sortingService, IFilteringService<Entry> filteringService, ISearchingService<Entry> searchingService) : base(repository, unitOfWork, sortingService, pagingService, mapper, filteringService,searchingService)
        {
            _entryRepository = entryRepository;
            _mapper = mapper;
            _pagingService = pagingService;
            _sortingService = sortingService;
            _filteringService = filteringService;
            _searchingService = searchingService;
        }

        public async Task<CustomResponseDto<List<EntriesWithUserAndTitleDto>>> GetEntriesWithUserAndTitle()
        {
            var entries = await _entryRepository.GetEntriesWithUserAndTitle();
            if (entries == null)
            {
                throw new NotFoundException($"{typeof(Entry).Name} not found");
            }
            var entriesDto = _mapper.Map<List<EntriesWithUserAndTitleDto>>(entries);
            return CustomResponseDto<List<EntriesWithUserAndTitleDto>>.Success(200, entriesDto);
        }

        public async Task<CustomResponseDto<EntriesWithUserAndTitleDto>> GetEntryByIdWithUserAndTitle(int entryId)
        {
          

            var entry = await _entryRepository.GetEntryByIdWithUserAndTitle(entryId);
            if (entry == null)
            {
                throw new NotFoundException($"{typeof(Entry).Name} not found");
            }
            var entryDto = _mapper.Map<EntriesWithUserAndTitleDto>(entry);
            return CustomResponseDto<EntriesWithUserAndTitleDto>.Success(200, entryDto);
        }
    }
}
