using AzureFunctionWebClient.Models;
using System.Text.Json;

namespace AzureFunctionWebClient.Services
{
    public class ProductService : IProductService
    {
        

        public Task<bool> IsBeta()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetProducts()
        {
            string functionUrl = "https://olusqlfunctiongetproducts.azurewebsites.net/api/GetProducts?code=Sh4z2zpXzhdG9cok1ITYG5oj3GckfuhbEFwBhmrsNMpKAzFuBE1bWw==";
            List<Product> products;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(functionUrl);
                var content = await response.Content.ReadAsStringAsync();

                products = JsonSerializer.Deserialize<List<Product>>(content)!;
            }

            return products;
        }

        
    }
}
