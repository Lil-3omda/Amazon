using AutoMapper;
using Amazon_API.Models.Entities.Carting;
using Amazon_API.Models.Entities.Carting.Dtos;

namespace Amazon_API.Mappings
{
    public class CartMappingProfile : Profile
    {
        public CartMappingProfile()
        {
            // Mapping from CartItem to CartItemResponseDto
            CreateMap<CartItem, CartItemResponseDto>()
                .ForMember(dest => dest.ProductTitle, opt => opt.MapFrom(src => src.Product.Title))
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.Product.Images.FirstOrDefault().ImageUrl))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.Price));

            // Mapping from CartItemCreateDto to CartItem
            CreateMap<CartItemCreateDto, CartItem>();
        }
    }
}
