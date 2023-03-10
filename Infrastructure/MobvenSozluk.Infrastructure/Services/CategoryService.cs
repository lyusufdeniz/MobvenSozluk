using AutoMapper;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Infrastructure.Exceptions;
using MobvenSozluk.Persistance.Repositories;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Repositories;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Repository.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Infrastructure.Services
{
    public class CategoryService : Service<Category,CategoryDto>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IPagingService<Category> _pagingService;
        private readonly ISortingService<Category> _sortingService;
        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper, IPagingService<Category> pagingService, ISortingService<Category> sortingService) : base(repository, unitOfWork,sortingService,pagingService,mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _pagingService = pagingService;
            _sortingService = sortingService;
        }

        public async Task<CustomResponseDto<CategoryByIdWithTitlesDto>> GetCategoryByIdWithTitles(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdWithTitles(categoryId);
            if (category == null)
            {
                throw new NotFoundException($"{typeof(Category).Name} not found");
            }
            var categoryDto = _mapper.Map<CategoryByIdWithTitlesDto>(category);
            return CustomResponseDto<CategoryByIdWithTitlesDto>.Success(200, categoryDto);
        }
    }
}
