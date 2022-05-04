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
using System.Threading.Tasks;
using System.Collections.Generic;
 
 
namespace LocalFunctionProj
{
    public static class HttpExample
    {
        [FunctionName("HttpExample")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
 
            string name = req.Query["name"];
 
            // Get the connection string from app settings and use it to create a connection.
            var str = "Server=tcp:msdocs-azuresql-server-524524804.database.windows.net,1433;Initial Catalog=msdocsazuresqldb524524804;Persist Security Info=False;User ID=azureuser;Password=Pa322w0rD-524524804;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var res = new List<string>();
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                var text = "select name from [SalesLT].[ProductCategory];";
                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                        SqlDataReader reader = cmd.ExecuteReader();
                                if (reader.HasRows)
                                {
                                        while (reader.Read())
                                        {
                                                res.Add(reader.GetString(0));
                                        }
                                }
                        reader.Close();
                        conn.Close();
                 }
            }
            return new OkObjectResult(res);
        }
    }
}

