using Microsoft.AspNetCore.Identity;
using Amazon_API.Models.Entities.Reviews;
using Amazon_API.Models.Entities.Products;
using Amazon_API.Models.Entities.Ordering;
using Amazon_API.Models.Entities.Carting;
using Amazon_API.Models.Entities.Seller;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amazon_API.Models.Entities.User
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostelCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ProfileImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginDate { get; set; }
        public string OtpCode { get; set; }
        public DateTime? OtpGeneratedAt { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<WishListItem> wishListItems { get; set; } = new List<WishListItem>();

        public SellerProfile? SellerProfile { get; set; }


    }
}
