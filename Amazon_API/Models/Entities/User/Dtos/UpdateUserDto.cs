namespace Amazon_API.Models.Entities.User.Dtos
{
    public class UpdateUserDto
    {
        public string FullName { get; set; }
        public UserAddressDto Address { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
