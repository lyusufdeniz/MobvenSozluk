using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Infrastructure.Services;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Services;

namespace MobvenSozluk.API.Controllers
{

    public class CategoryController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _service;
        private readonly IPagingService<Category> _pagingService;

        public CategoryController(IMapper mapper, ICategoryService categoryService, IPagingService<Category> pagingService)
        {
            _mapper = mapper;
            _service = categoryService;
            _pagingService = pagingService;
        }

        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetCategoryByIdWithTitles(int categoryId)
        {
            return CreateActionResult(await _service.GetCategoryByIdWithTitles(categoryId));
        }
        [HttpGet]
        public async Task<IActionResult> All(int pageNo,int pageSize)
        {

            return Ok( _pagingService.GetPaged( await _service.GetAllAsync(), pageNo, pageSize).Item2);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);

            var categoriesDto = _mapper.Map<CategoryDto>(category);

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(200, categoriesDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            var category = await _service.AddAsync(_mapper.Map<Category>(categoryDto));

            var categoriesDto = _mapper.Map<CategoryDto>(category);

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(200, categoriesDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            await _service.UpdateAsync(_mapper.Map<Category>(categoryDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var category = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(category);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
