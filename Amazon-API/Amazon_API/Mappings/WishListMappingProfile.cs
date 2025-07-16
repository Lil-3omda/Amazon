using Amazon_API.Models.Entities.Carting;
using Amazon_API.Models.Entities.Carting.Dtos;
using AutoMapper;

namespace Amazon_API.Mappings
{
    public class WishListMappingProfile : Profile
    {
        public WishListMappingProfile()
        {
            CreateMap<WishListItem, WishListItemResponseDto>()
                .ForMember(dest => dest.ProductTitle, opt => opt.MapFrom(src => src.Product.Title))
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.Product.Images.FirstOrDefault().ImageUrl))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));

            CreateMap<WishListItemCreateDto, WishListItem>();
        }
    }
}
