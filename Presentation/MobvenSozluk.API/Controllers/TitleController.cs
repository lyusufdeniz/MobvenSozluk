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
    
    public class TitleController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly ITitleService _service;

        public TitleController(IMapper mapper, ITitleService titleService)
        {
            _mapper = mapper;
            _service = titleService;
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
        public async Task<IActionResult> All()
        {
            var titles = await _service.GetAllAsync();

            var titleDtos = _mapper.Map<List<TitleDto>>(titles.ToList());

            return CreateActionResult(CustomResponseDto<List<TitleDto>>.Success(200, titleDtos));
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
