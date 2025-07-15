namespace Amazon_API.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> SoftDeleteProductAsync(int productId);
    }
}
