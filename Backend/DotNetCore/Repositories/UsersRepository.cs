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
    }
}
