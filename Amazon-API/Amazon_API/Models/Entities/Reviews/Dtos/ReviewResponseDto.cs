namespace Amazon_API.Models.Entities.Reviews.Dtos
{
    public class ReviewResponseDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string UserProfileImageUrl { get; set; }

        public int Rating { get; set; }
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
