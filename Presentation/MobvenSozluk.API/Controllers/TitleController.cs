using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Services;

namespace MobvenSozluk.API.Controllers
{
    public class TitleController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly ITitleService _service;
        private readonly IPagingService<Title> _pagingService;
        private readonly ISortingService<Title> _sortingService;

        public TitleController(IMapper mapper, ITitleService titleService, IPagingService<Title> pagingService, ISortingService<Title> sortingService)
        {
            _mapper = mapper;
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
        public async Task<IActionResult> All(int pageNo, int pageSize, bool sortByDesc, string sortParameter)
        {
            

            var data = await _service.GetAllAsync();
            var sortservice=_sortingService.Sort(data,sortByDesc,sortParameter);
            var pageddata = _pagingService.GetPaged(sortservice.Item2, pageNo, pageSize);
            var pagingdetail = pageddata.Item1;
            var titles = pageddata.Item2;
            var titlesdto = _mapper.Map<List<TitleDto>>(titles);
            return CreateActionResult(CustomResponseDto<List<TitleDto>>.Success(200, titlesdto, pagingdetail,sortservice.Item1));



        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var title = await _service.GetByIdAsync(id);

            var titlesDto = _mapper.Map<TitleDto>(title);

            return CreateActionResult(CustomResponseDto<TitleDto>.Success(200, titlesDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(TitleDto titleDto)
        {
            var title = await _service.AddAsync(_mapper.Map<Title>(titleDto));

            var titlesDto = _mapper.Map<TitleDto>(title);

            return CreateActionResult(CustomResponseDto<TitleDto>.Success(200, titlesDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(TitleDto titleDto)
        {
            await _service.UpdateAsync(_mapper.Map<Title>(titleDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var title = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(title);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
