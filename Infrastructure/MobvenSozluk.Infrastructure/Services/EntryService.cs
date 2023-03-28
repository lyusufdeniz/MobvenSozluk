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
        private readonly IErrorMessageService _errorMessageService;
        public EntryService(IGenericRepository<Entry> repository, IUnitOfWork unitOfWork, IEntryRepository entryRepository, IMapper mapper, IPagingService<Entry> pagingService, ISortingService<Entry> sortingService, IFilteringService<Entry> filteringService, ISearchingService<Entry> searchingService, IErrorMessageService errorMessageService) : base(repository, unitOfWork, sortingService, pagingService, mapper, filteringService, searchingService, errorMessageService)
        {
            _entryRepository = entryRepository;
            _mapper = mapper;
            _pagingService = pagingService;
            _sortingService = sortingService;
            _filteringService = filteringService;
            _searchingService = searchingService;
            _errorMessageService = errorMessageService;
        }

        public async Task<CustomResponseDto<List<EntriesWithUserAndTitleDto>>> GetEntriesWithUserAndTitle()
        {
            var entries = await _entryRepository.GetEntriesWithUserAndTitle();
            if (entries == null)
            {
                throw new NotFoundException(_errorMessageService.NotFoundMessage<Entry>());
            }
            var entriesDto = _mapper.Map<List<EntriesWithUserAndTitleDto>>(entries);
            return CustomResponseDto<List<EntriesWithUserAndTitleDto>>.Success(200, entriesDto);
        }

        public async Task<CustomResponseDto<EntriesWithUserAndTitleDto>> GetEntryByIdWithUserAndTitle(int entryId)
        {
          

            var entry = await _entryRepository.GetEntryByIdWithUserAndTitle(entryId);
            if (entry == null)
            {
                throw new NotFoundException(_errorMessageService.NotFoundMessage<Entry>());
            }
            var entryDto = _mapper.Map<EntriesWithUserAndTitleDto>(entry);
            return CustomResponseDto<EntriesWithUserAndTitleDto>.Success(200, entryDto);
        }
    }
}
