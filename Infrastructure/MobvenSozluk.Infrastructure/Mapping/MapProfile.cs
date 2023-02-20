using AutoMapper;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs;

namespace MobvenSozluk.Infrastructure.Mapping;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Entry, EntryDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<Role, RoleDTO>().ReverseMap();
        CreateMap<Title, TitleDTO>().ReverseMap();
        
    }
}