using DotNetCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore.Repositories
{
    public class MedicinesRepository
    {
        public Response addToCart(Cart cart, SqlConnection con)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddToCart", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", cart.UserId);
            cmd.Parameters.AddWithValue("@MedicineId", cart.MedicineId);
            cmd.Parameters.AddWithValue("@UnitPrice", cart.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", cart.Discount);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
            cmd.Parameters.AddWithValue("@TotalPrice", cart.TotalPrice);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Item added successfully!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Item could not be added";
            }
            return response;
        }

        public Response placeOrder(Users users, SqlConnection con)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_placeOrder", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", users.ID);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Order has been placed successfully!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order could not be placed";
            }

            return response;
        }
    }
}
