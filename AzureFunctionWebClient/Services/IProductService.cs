using AzureFunctionWebClient.Models;

namespace AzureFunctionWebClient.Services
{
    public interface IProductService
    {
          Task<List<Product>> GetProducts();
        Task<bool> IsBeta();
    }
}
