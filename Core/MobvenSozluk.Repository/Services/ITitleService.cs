﻿using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.Services
{
    public interface ITitleService: IService<Title,TitleDto>
    {
        Task<CustomResponseDto<List<TitlesWithUserAndCategoryDto>>> GetTitlesWithUserAndCategory();
        Task<CustomResponseDto<TitleByIdWithEntriesDto>> GetTitleByIdWithEntries(int titleId);
    }
}
