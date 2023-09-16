using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Tatweer_Test.Models
{
    public class DAL
    {
        public Response Registration(Users users, SqlConnection conn)
        {
            Response response = new Response();

            try
            {
                SqlCommand cmd = new SqlCommand("sp_register", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", users.Username);
                cmd.Parameters.AddWithValue("@Password", users.Password);
                cmd.Parameters.AddWithValue("@Email", users.Email);
                cmd.Parameters.AddWithValue("@Mobile", users.Mobile);
                cmd.Parameters.Add("@ErrorMessage", System.Data.SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = System.Data.ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string message = (string)cmd.Parameters["@ErrorMessage"].Value;
                if (i > 0)
                {
                    response.statusCode = 200;
                    response.statusMessage = message;

                }
                else
                {
                    response.statusCode = 100;
                    response.statusMessage = message;

                }

            }
            catch (Exception ex)
            {
                response.statusCode = 100;
                response.statusMessage = ex.Message;
            }


            return response;

        }

        public Response Login(login users, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("sp_login", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
                da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    response.statusCode = 200;
                    response.statusMessage = "User Is Valid";
                    
                }
                else
                {
                    response.statusCode = 100;
                    response.statusMessage = "User Is Invalid";
                }

            }
            catch (Exception ex)
            {
                response.statusCode = 100;
                response.statusMessage = ex.Message;
            }


            return response;

        }
        public Response LoginAuthDal(login users, SqlConnection conn)
        {
            Response response = new Response();
            //login _user = null;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("sp_login", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
                da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    response.statusCode = 200;
                    response.statusMessage = "User Is Valid";
                    //_user=new login { Email=users.Email };


                }
                else
                {
                    response.statusCode = 100;
                    response.statusMessage = "User Is Invalid";
                }

            }
            catch (Exception ex)
            {
                response.statusCode = 100;
                response.statusMessage = ex.Message;
            }


            return response;

        }

        public Response AddData(Chart chart, SqlConnection conn)
        {
            Response response = new Response();

            try
            {
                SqlCommand cmd = new SqlCommand("sp_ChartData", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Date", chart.Date1);
                cmd.Parameters.AddWithValue("@Value1", chart.Value1);
                cmd.Parameters.AddWithValue("@Value2", chart.Value2);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                if (i > 0)
                {
                    response.statusCode = 200;
                    response.statusMessage = "Added";

                }
                else
                {
                    response.statusCode = 100;
                    response.statusMessage = "Error While Adding";
                }
            }
            catch (Exception ex)
            {
                response.statusCode = 100;
                response.statusMessage = ex.Message;
            }

            return response;
        }

        public Response EditData(Chart chart, SqlConnection conn)
        {
            Response response = new Response();

            try
            {
                SqlCommand cmd = new SqlCommand("sp_EditChartData", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", chart.ID);
                cmd.Parameters.AddWithValue("@ChartDate", chart.Date1);
                cmd.Parameters.AddWithValue("@NewValue1", chart.Value1);
                cmd.Parameters.AddWithValue("@NewValue2", chart.Value2);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                if (i > 0)
                {
                    response.statusCode = 200;
                    response.statusMessage = "Updated";
                }
                else
                {
                    response.statusCode = 100;
                    response.statusMessage = "Error While Updated";
                }
            }
            catch (Exception ex)
            {
                response.statusCode = 100;
                response.statusMessage = ex.Message;
            }

            return response;
        }

        public List<Chart> GetAllChartData(Chart chart,SqlConnection conn)
        {
            List<Chart> chartData = new List<Chart>();

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM chartData", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    chart = new Chart();
                    chart.ID = (int)reader["ID"];
                    chart.Date1 = (string) reader["Date"];
                    chart.Value1 = (decimal)reader["Value1"];
                    chart.Value2 = (decimal)reader["Value2"];

                    chartData.Add(chart);
                }

                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
            }

            return chartData;
        }
    }
}
