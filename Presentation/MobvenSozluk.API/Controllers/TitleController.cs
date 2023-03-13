using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.RequestDTOs;
using MobvenSozluk.Repository.Services;

namespace MobvenSozluk.API.Controllers
{
    public class TitleController : CustomBaseController
    {

        private readonly ITitleService _service;
        private readonly IPagingService<Title> _pagingService;
        private readonly ISortingService<Title> _sortingService;


        public TitleController( ITitleService titleService, IPagingService<Title> pagingService, ISortingService<Title> sortingService)
        {
         
            _service = titleService;
            _pagingService = pagingService;
            _sortingService = sortingService;   
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTitlesWithUserAndCategory()
        {
            return CreateActionResult(await _service.GetTitlesWithUserAndCategory());
           
        }

        [HttpGet("[action]/{titleId}")]
        public async Task<IActionResult> GetTitleByIdWithEntries(int titleId)
        {

            return CreateActionResult(await _service.GetTitleByIdWithEntries(titleId));

        }

        [HttpGet]
        public async Task<IActionResult> All(int pageNo, int pageSize, bool sortByDesc, string sortParameter,[FromQuery] List<FilterDTO> Filters)
        {

            return CreateActionResult(await _service.GetAllAsync(sortByDesc, sortParameter, pageNo, pageSize, Filters));


        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save(TitleDto titleDto)
        {
            return CreateActionResult(await _service.AddAsync(titleDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(TitleDto titleDto)
        {
            return CreateActionResult(await _service.UpdateAsync(titleDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {

            return  CreateActionResult(await _service.RemoveAsync(id));
        }
    }
}
