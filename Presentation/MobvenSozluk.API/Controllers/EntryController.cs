using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.RequestDTOs;
using MobvenSozluk.Repository.Services;

namespace MobvenSozluk.API.Controllers
{
    
    public class EntryController : CustomBaseController
    {
       
        private readonly IEntryService _service;

        public EntryController( IEntryService entryService)
        {
          
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
        public async Task<IActionResult> All(int pageNo, int pageSize, bool sortByDesc, string sortParameter, List<FilterDTO> filters)
        {

            return CreateActionResult(await _service.GetAllAsync(sortByDesc, sortParameter, pageNo, pageSize, filters));


        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            return CreateActionResult(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save(EntryDto entryDto)
        {

            return CreateActionResult(await _service.AddAsync(entryDto));
        }
 
        [HttpPut]
        public async Task<IActionResult> Update(EntryDto entryDto)
        {
            return CreateActionResult(await _service.UpdateAsync(entryDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }
    }
}