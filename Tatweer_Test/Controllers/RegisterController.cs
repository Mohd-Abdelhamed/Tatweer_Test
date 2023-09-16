using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tatweer_Test.Models;

namespace Tatweer_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        SqlConnection conn = null;
        public RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("EmployeeAppCon").ToString());
        }
        public string GenerateJwtToken(login users)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credintails = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
                {
                    new Claim(type: JwtRegisteredClaimNames.Sub, value: users.Email),
                    new Claim(type: JwtRegisteredClaimNames.Email, value: users.Email),
                    new Claim(type: JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(type: JwtRegisteredClaimNames.Iat, value: DateTime.Now.ToUniversalTime().ToString())
                };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                    claims,
                    null,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credintails);

            string Token = new JwtSecurityTokenHandler().WriteToken(token);
            return Token;

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(login log)
        {
            Response res = new Response();
            IActionResult response = Unauthorized();

            DAL dal = new DAL();
            res = dal.LoginAuthDal(log, conn);
            if (res.statusCode == 200)
            {
                var token = GenerateJwtToken(log);
                res.accessToken = token;
                HttpContext.Session.SetString("JWToken", token);

                response = Ok(res); // Return the response with the generated token

            }

            return response;
        }

        [HttpPost]
        [Route("Registration")]
        public Response Registration(Users users)
        {
            Response response = new Response();
            DAL dal = new DAL();
            response = dal.Registration(users, conn);
            return response;
        }

        [Authorize]
        [HttpPost]
        [Route("AddValues")]
        public Response AddValues(Chart chart)
        {
            Response response = new Response();
            DAL dal = new DAL();
            response = dal.AddData(chart, conn);
            return response;
        }

        [Authorize]
        [HttpPut]
        [Route("EditValues")]
        public Response EditValues(List<Chart> chartData)
        {
            Response response = new Response();
            DAL dal = new DAL();
            foreach (var chart in chartData)
            {
                response = dal.EditData(chart, conn);
                if (response.statusCode != 200)
                {
                    // If an error occurs for any item, return the response immediately
                    return response;
                }
            }

            return response;
        }

        [Authorize]
        [HttpGet]
        [Route("GetChartData")]
        public IActionResult GetChartData()
        {
            DAL dal = new DAL();
            List<Chart> chartData = dal.GetAllChartData(new Chart(), conn);
            var ids = chartData.Select(c => c.ID).ToArray();
            var labels = chartData.Select(c => c.Date1).ToArray();
            var values1 = chartData.Select(c => c.Value1).ToArray();
            var values2 = chartData.Select(c => c.Value2).ToArray();

            var data = new
            {
                ids = ids,
                labels = labels,
                values1 = values1,
                values2 = values2
            };

            return Ok(data);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // Perform logout actions, such as clearing authentication cookies or tokens

            // Redirect the user to a specific page after logout (e.g., home page)
            await HttpContext.SignOutAsync("CustomCookieScheme");
            return RedirectToAction("Index", "Home");
        }


    }
}
