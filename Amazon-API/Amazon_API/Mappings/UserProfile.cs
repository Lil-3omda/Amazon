using Amazon_API.Models.Entities.User.Dtos;
using Amazon_API.Models.Entities.User;
using AutoMapper;
namespace Amazon_API.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserProfileDto>()
                .ReverseMap();

            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<UpdateUserDto, ApplicationUser>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.PostelCode, opt => opt.MapFrom(src => src.Address.PostelCode))
                .ReverseMap();
        }
    }
}
