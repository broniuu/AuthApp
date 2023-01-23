using AuthApp.Dtos;
using AuthApp.Model;
using AutoMapper;

namespace AuthApp.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AppUser, UserDto>();
        CreateMap<AppUser, MemberDto>();
    }
}