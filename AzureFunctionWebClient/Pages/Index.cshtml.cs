using AzureFunctionWebClient.Models;
using AzureFunctionWebClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureFunctionWebClient.Pages
{
    public class IndexModel : PageModel
    {
        public IList<Product> Products { get; set; } = default;

        private readonly ILogger<IndexModel> _logger;
        private readonly IProductService _productService;
        public IndexModel(ILogger<IndexModel> logger, IProductService productService )
        {
            _logger = logger;
            _productService = productService;
        }

        public void OnGet()
        {
            var products = _productService.GetProducts().Result;
            Products =   products;
        }
    }
}