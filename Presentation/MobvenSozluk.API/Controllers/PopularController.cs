using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Services;

namespace MobvenSozluk.API.Controllers;

public class PopularController : CustomBaseController
{
    private readonly IMapper _mapper;
    private readonly ITitleService _service;

    public PopularController(IMapper mapper, ITitleService service)
    {
        _mapper = mapper;
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPopularTitles()
    {
        var popularTitles = await _service.GetPopularTitlesAsync();
        var titleDtos = _mapper.Map<List<TitleDto>>(popularTitles.ToList());
        return CreateActionResult(CustomResponseDto<List<TitleDto>>.Success(200, titleDtos));
    }

}