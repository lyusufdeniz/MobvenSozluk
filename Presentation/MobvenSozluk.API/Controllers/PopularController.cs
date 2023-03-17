using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Services;

namespace MobvenSozluk.API.Controllers;

public class PopularController : CustomBaseController
{
    private readonly ITitleService _service;

    public PopularController(ITitleService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPopularTitles()
    {
        var popularTitles = await _service.GetPopularTitlesAsync();
        return CreateActionResult(CustomResponseDto<List<TitleDto>>.Success(200, popularTitles));
    }

}