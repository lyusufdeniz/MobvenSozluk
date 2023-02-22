using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Infrastructure.Services;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;

namespace MobvenSozluk.API.Controllers
{

    public class EntryController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IEntryService _service;

        public EntryController(IMapper mapper, IEntryService entryService)
        {
            _mapper = mapper;
            _service = entryService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetEntriesWithUserAndTitle()
        {
            return CreateActionResult(await _service.GetEntriesWithUserAndTitle());
        }

        [HttpGet("[action]/{entryId}")]
        public async Task<IActionResult> GetEntryByIdWithUserAndTitle(int entryId)
        {
            return CreateActionResult(await _service.GetEntryByIdWithUserAndTitle(entryId));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var entries = await _service.GetAllAsync();

            var entriesDtos = _mapper.Map<List<EntryDto>>(entries.ToList());

            return CreateActionResult(CustomResponseDto<List<EntryDto>>.Success(200, entriesDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entry = await _service.GetByIdAsync(id);

            var entriesDto = _mapper.Map<EntryDto>(entry);

            return CreateActionResult(CustomResponseDto<EntryDto>.Success(200, entriesDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(EntryDto entryDto)
        {
            var entry = await _service.AddAsync(_mapper.Map<Entry>(entryDto));

            var entriesDto = _mapper.Map<EntryDto>(entry);

            return CreateActionResult(CustomResponseDto<EntryDto>.Success(200, entriesDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(EntryDto entryDto)
        {
            await _service.UpdateAsync(_mapper.Map<Entry>(entryDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var entry = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(entry);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
