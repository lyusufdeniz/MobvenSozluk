using AutoMapper;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Persistance.Repositories;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
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
    public class TitleService : Service<Title>, ITitleService
    {
        private readonly ITitleRepository _titleRepository;
        private readonly IMapper _mapper;
        public TitleService(IGenericRepository<Title> repository, IUnitOfWork unitOfWork, ITitleRepository titleRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _titleRepository = titleRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<TitleByIdWithEntriesDto>> GetTitleByIdWithEntries(int titleId)
        {
            var title = await _titleRepository.GetTitleByIdWithEntries(titleId);
            var titleDto = _mapper.Map<TitleByIdWithEntriesDto>(title);
            return CustomResponseDto<TitleByIdWithEntriesDto>.Success(200, titleDto);
        }

        public async Task<CustomResponseDto<List<TitlesWithUserAndCategoryDto>>> GetTitlesWithUserAndCategory()
        {
            var titles = await _titleRepository.GetTitlesWithUserAndCategory();
            var titlesDto = _mapper.Map<List<TitlesWithUserAndCategoryDto>>(titles);
            return CustomResponseDto<List<TitlesWithUserAndCategoryDto>>.Success(200, titlesDto);
        }
    }
}
