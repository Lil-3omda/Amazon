namespace Amazon_API.Services.Interfaces
{
    public interface IProductImageService
    {
        Task<bool> SoftDeleteImageAsync(int productid,int imageId);
    }
}
