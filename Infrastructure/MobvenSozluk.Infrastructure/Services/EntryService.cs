using AutoMapper;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Persistance.Repositories;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Repositories;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Repository.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Infrastructure.Services
{
    public class EntryService : Service<Entry>, IEntryService
    {
        private readonly IEntryRepository _entryRepository;
        private readonly IMapper _mapper;
        public EntryService(IGenericRepository<Entry> repository, IUnitOfWork unitOfWork, IEntryRepository entryRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _entryRepository = entryRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<EntriesWithUserAndTitleDto>>> GetEntriesWithUserAndTitle()
        {
            var entries = await _entryRepository.GetEntriesWithUserAndTitle();
            var entriesDto = _mapper.Map<List<EntriesWithUserAndTitleDto>>(entries);
            return CustomResponseDto<List<EntriesWithUserAndTitleDto>>.Success(200, entriesDto);
        }

        public async Task<CustomResponseDto<EntriesWithUserAndTitleDto>> GetEntryByIdWithUserAndTitle(int entryId)
        {
            var entry = await _entryRepository.GetEntryByIdWithUserAndTitle(entryId);
            var entryDto = _mapper.Map<EntriesWithUserAndTitleDto>(entry);
            return CustomResponseDto<EntriesWithUserAndTitleDto>.Success(200, entryDto);
        }
    }
}
