using Amazon_API.Models.Entities.Products.Dtos;
using Amazon_API.Models.Entities.Products;
using AutoMapper;
using Amazon_API.Models.Entities.Reviews;

namespace Amazon_API.Mappings.Products
{
    public class ProductProfile :Profile
    {
        public ProductProfile()  
        {
            // Map from CreateDto to Product
            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.Images,
                    opt => opt.MapFrom(src => src.ImageUrls.Select(url => new ProductImage { ImageUrl = url })));

            // Map from Product to ResponseDto
            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.ImageUrls,
                    opt => opt.MapFrom(src => src.Images.Select(img => img.ImageUrl)))
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
                .ForMember(dest => dest.SellerName,
                    opt => opt.MapFrom(src => src.Seller != null ? src.Seller.UserName : string.Empty));

            //  Update product
            CreateMap<UpdateProductDto, Product>();

            //update review
            CreateMap<UpdateReviewDto, Review>();

        }
    }
}
