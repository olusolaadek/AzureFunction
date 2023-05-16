using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SQLFunctionApp
{
    public static class GetProduct
    {
        [FunctionName("GetProducts")]
        public static async Task<IActionResult> RunGetProducts(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log) // , "post"
        {
            List<Product> products = new List<Product>();
            string statement = "SELECT ProductId, ProductName, Quantity from PRODUCTS";
            SqlConnection conn = SqlUtilityClass.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand(statement, conn);
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Product product = new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                    };

                    products.Add(product);
                }
            }

            conn.Close();
            return new OkObjectResult(products);
            //log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

            // return new OkObjectResult(responseMessage);
        }

        //[FunctionName("GetNewProducts")]
        //public static async Task<List<Product>> RunGetNewProducts()
        //{
        //    string functionUrl = "";

        //}

       [FunctionName("GetProduct")]
        public static async Task<IActionResult> RunGetProduct(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
           ILogger log) //, "post"
        {
            int productId = Int32.Parse( req.Query["id"]);
            
            string statement = String.Format("SELECT ProductId, ProductName, Quantity from PRODUCTS where ProductId = {0}", productId);
            SqlConnection conn = SqlUtilityClass.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand(statement, conn);
            Product product;
            try
            {
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    await reader.ReadAsync();

                    product = new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                    };

                }
                return new OkObjectResult(product);
            }
            catch (Exception ex)
            {
                return new OkObjectResult("No product found ");
            }
            

            conn.Close();
            

        }
            
    }
}
