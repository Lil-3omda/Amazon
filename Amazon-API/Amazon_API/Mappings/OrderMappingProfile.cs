using Amazon_API.Models.Entities.Ordering;
using Amazon_API.Models.Entities.Ordering.Dtos;
using AutoMapper;

namespace Amazon_API.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile() 
        {
            CreateMap<Order, OrderResponseDto>();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductTitle, opt => opt.MapFrom(src => src.Product.Title));

            CreateMap<OrderCreateDto, Order>();

            CreateMap<OrderItemCreateDto, OrderItem>();
        }
    }
}
