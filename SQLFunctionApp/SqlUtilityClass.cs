using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFunctionApp
{
    public static class SqlUtilityClass
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_SqlConnectionString"); // @"data source=.; database=Demo_db; integrated security=SSPI";
            return new SqlConnection(connectionString);
        }
    }
}
