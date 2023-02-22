using AutoMapper;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Infrastructure.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryByIdWithTitlesDto>();

            CreateMap<Entry, EntryDto>().ReverseMap();
            CreateMap<Entry, EntriesWithUserAndTitleDto>();

            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Role, RoleByIdWithUsersDto>();

            CreateMap<Title, TitleDto>().ReverseMap();
            CreateMap<Title, TitleByIdWithEntriesDto>();
            CreateMap<Title, TitlesWithUserAndCategoryDto>();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UsersWithRoleDto>();
            CreateMap<User, UserByIdWithEntriesDto>();
            
            
        }
    }
}
