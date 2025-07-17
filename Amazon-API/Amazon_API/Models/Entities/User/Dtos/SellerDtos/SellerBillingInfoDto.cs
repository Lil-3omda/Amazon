namespace Amazon_API.Models.Entities.User.Dtos.SellerDtos
{
    public class SellerBillingInfoDto
    {
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CardHolderName { get; set; }
    }
}
