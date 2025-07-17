namespace Amazon_API.Services.Interfaces
{
    public interface IReviewService
    {
        Task<bool> SoftDeleteReviewAsync(int productid, int reviewid);
    }
}
