using Amazon_API.Models.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amazon_API.Models.Entities.Seller
{
    public class SellerProfile
    {
        [Key]
        public int Id { get; set; }

        // Foreign key to ApplicationUser
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        // Page 1: Business Info
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public string CountryOfRegistration { get; set; }

        // Page 2: Identity Info
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
        public string AddressLine2 { get; set; }
        public string Governorate { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }

        // Page 3: Billing
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string CardHolderName { get; set; }

        // Page 4: Store 

        // Page 5: Verification 

        public bool IsApproved { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
