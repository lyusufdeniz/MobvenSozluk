using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
            CreateMap<Role, RoleByIdWithUsersDto>()
                .ForMember(dest => dest.Users, opt => opt
                .MapFrom(src => src.UserRoles.Select(ur => ur.User)));

            CreateMap<Title, TitleDto>().ReverseMap();
            CreateMap<Title, TitleByIdWithEntriesDto>();
            CreateMap<Title, TitlesWithUserAndCategoryDto>();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, AddUserDto>().ReverseMap();     
            CreateMap<User, UsersWithRoleDto>()
                .ForMember(dest => dest.Roles, opt => opt
                .MapFrom(src => src.UserRoles.Select(ur => new RoleDto
                 {
                   Id = ur.Role.Id,
                   Name = ur.Role.Name
                 }).ToList()));
            CreateMap<User, UserByIdWithEntriesDto>();
            CreateMap<User, UserByIdWithTitlesDto>();
            CreateMap<User, UserDtoWithToken>();
            //CreateMap<User, RegisterDto>();
            
            
        }
    }
}
