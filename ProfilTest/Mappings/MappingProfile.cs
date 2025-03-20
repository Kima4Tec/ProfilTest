using AutoMapper;
using ProfilTest.DTOs;
using ProfilTest.Models;

namespace ProfilTest.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Profiles, ProfilDto>().ReverseMap();
            CreateMap<Emails, EmailDto>().ReverseMap();
        }
    }
}
