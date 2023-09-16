using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using Tatweer_Test.Models;

/*using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
*/

namespace Tatweer_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        SqlConnection conn = null;


        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("EmployeeAppCon").ToString());
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            List<int> ids = new List<int>();
            List<string> labels = new List<string>();
            List<decimal> values1 = new List<decimal>();
            List<decimal> values2 = new List<decimal>();

            // Retrieve connection string from appsettings.json
            string connectionString = _configuration.GetConnectionString("EmployeeAppCon");

            // Query the database to retrieve chart data
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ID, Date, Value1, Value2 FROM chartData";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            ids.Add((int)reader["ID"]);
                            labels.Add(reader["Date"].ToString());
                            values1.Add((decimal)reader["Value1"]);
                            values2.Add((decimal)reader["Value2"]);
                        }
                    }
                }
            }

            // Create a list of Chart objects to hold the data
            List<Chart> chartData = new List<Chart>();

            for (int i = 0; i < labels.Count; i++)
            {
                Chart chart = new Chart
                {
                    ID = ids[i],
                    Date1 = labels[i],
                    Value1 = values1[i],
                    Value2 = values2[i]
                };

                chartData.Add(chart);
            }

            // Pass the retrieved data to the view
            ViewBag.Labels = labels;
            ViewBag.Values1 = values1;
            ViewBag.Values2 = values2;
            ViewBag.ChartData = chartData;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}