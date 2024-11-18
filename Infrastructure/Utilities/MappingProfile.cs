using Application.DTO;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UsersView, UsersViewDTO>()
                .ForMember(x => x.PasswordHash, opt => opt.Ignore())
                .ForMember(x => x.PasswordSalt, opt => opt.Ignore());
        }
    }
}
