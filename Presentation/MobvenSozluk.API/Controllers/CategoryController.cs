using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.RequestDTOs;
using MobvenSozluk.Repository.Services;

namespace MobvenSozluk.API.Controllers
{
    
    public class CategoryController : CustomBaseController
    {

        private readonly ICategoryService _service;
        private readonly IPagingService<Category> _pagingService;

        public CategoryController( ICategoryService categoryService, IPagingService<Category> pagingService)
        {
          
            _service = categoryService;
            _pagingService = pagingService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Search(int pageNo, int pageSize, string query)
        {
            return CreateActionResult(await _service.Search(pageNo, pageSize, query));
        }



        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetCategoryByIdWithTitles(int categoryId)
        {
            return CreateActionResult(await _service.GetCategoryByIdWithTitles(categoryId));
        }
        [HttpPost]
        public async Task<IActionResult> All(int pageNo, int pageSize, bool sortByDesc, string sortParameter, List<FilterDTO>? Filters)
        {

            return CreateActionResult(await _service.GetAllAsync(sortByDesc, sortParameter, pageNo, pageSize, Filters));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _service.GetByIdAsync(id));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {

            return CreateActionResult(await _service.AddAsync(categoryDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
           

            return CreateActionResult(await _service.UpdateAsync(categoryDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {

            return CreateActionResult(await _service.RemoveAsync(id));
        }

    }
}
