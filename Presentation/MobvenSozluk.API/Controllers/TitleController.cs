using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs.CustomResponse;
using MobvenSozluk.Repository.DTOs;
using MobvenSozluk.Repository.Services;

namespace MobvenSozluk.API.Controllers
{
    public class TitleController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Title> _service;

        public TitleController(IMapper mapper, IService<Title> service)
        {
            _mapper = mapper;
            _service = service;
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
