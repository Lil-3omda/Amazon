namespace Amazon_API.Models.Entities.User.Dtos.SellerDtos
{
    public class SellerIdentityInfoDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CountryOfCitizenship { get; set; }
        public string CountryOfBirth { get; set; }
        public string IdentityProof { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CountryOfIssue { get; set; }

        public string ResidentialAddress { get; set; }
        public string AddressLine1 { get; set; }
        public string Governorate { get; set; }
        public string Area { get; set; }
        public string AddressLine2 { get; set; }
        public string CityOrTown { get; set; }
        public string PhoneNumber { get; set; }
    }
}
