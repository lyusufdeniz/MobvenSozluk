using AutoMapper;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs;

namespace MobvenSozluk.Infrastructure.Mapping;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Entry, EntryDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<Title, TitleDto>().ReverseMap();
        
    }
}