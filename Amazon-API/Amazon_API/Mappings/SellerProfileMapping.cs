using AutoMapper;
using Amazon_API.Models.Entities.User.Dtos.SellerDtos;
using Amazon_API.Models.Entities.Seller;

namespace Amazon_API.Mappings
{
    public class SellerProfileMapping : Profile
    {
        public SellerProfileMapping()
        {
            CreateMap<SellerBusinessInfoDto, SellerProfile>();
            CreateMap<SellerIdentityInfoDto, SellerProfile>();
            CreateMap<SellerBillingInfoDto, SellerProfile>();
            CreateMap<SellerStoreInfoDto, SellerProfile>();
            CreateMap<SellerVerificationInfoDto, SellerProfile>();
        }
    }
}
