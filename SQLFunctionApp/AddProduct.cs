using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace SQLFunctionApp
{
    public static class AddProduct
    {
        [FunctionName("AddProduct")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Product product = JsonConvert.DeserializeObject<Product>(requestBody);

            var conn = SqlUtilityClass.GetConnection();

            
            try
            {
                string statement = "INSERT INTO PRODUCTS ( ProductName, Quantity) VALUES (@product_name, @price)";

                conn.Open();
                using (SqlCommand cmd = new SqlCommand(statement, conn))
                {
                    cmd.Parameters.Add("@product_name", SqlDbType.VarChar).Value = product.ProductName;
                    cmd.Parameters.Add("@price", SqlDbType.Int).Value = product.Quantity;
                    int result = cmd.ExecuteNonQuery();
                    return new OkObjectResult($"{result} products added");
                }
            }
            catch (Exception ex)
            {

                return new OkObjectResult("Error adding product - " + ex.Message) ;
            }

        }
    }
}
