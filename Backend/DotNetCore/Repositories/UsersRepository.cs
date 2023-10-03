using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DotNetCore.Models;

namespace DotNetCore.Repositories
{
    public class UsersRepository
    {
        public Response register(Users users, SqlConnection con)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Fund", 0);
            cmd.Parameters.AddWithValue("@Type", "Users");
            cmd.Parameters.AddWithValue("@Status", "Pending");
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if(i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User registered successfully!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User registration failed";
            }

            return response;
        }

        public Response login(Users users, SqlConnection con)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_login", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if(dt.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                response.StatusCode = 200;
                response.StatusMessage = "Logged in successfully!";
                response.user = user;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Invalid credentials";
                response.user = null;
            }
            return response;
        }

        public Response viewUser(Users users, SqlConnection con)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_viewUser", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                user.Fund = Convert.ToDecimal(dt.Rows[0]["Fund"]);
                user.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                user.Password = Convert.ToString(dt.Rows[0]["Password"]);
                response.StatusCode = 200;
                response.StatusMessage = "user exists";
                response.user = user;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User does not exit";
                response.user = null;
            }
            return response;

        }
        
        public Response updateProfile(Users users, SqlConnection con)
        {
            Response response = new Response();

            SqlCommand cmd = new SqlCommand("sp_updateProfile", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Record updated successfully!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Some error occured, try after some time";
            }

            return response;
        }
    }
}
