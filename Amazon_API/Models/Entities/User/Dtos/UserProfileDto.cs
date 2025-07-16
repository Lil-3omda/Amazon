namespace Amazon_API.Models.Entities.User.Dtos
{
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostelCode { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
