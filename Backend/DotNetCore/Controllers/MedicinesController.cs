using DotNetCore.Models;
using DotNetCore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MedicinesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("addToCart")]
        public Response addToCart(Cart cart)
        {
            MedicinesRepository mr = new MedicinesRepository();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = mr.addToCart(cart, con);
            return response;
        }

        [HttpPost]
        [Route("placeOrder")]
        public Response placeOrder(Users users)
        {
            MedicinesRepository mr = new MedicinesRepository();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = mr.placeOrder(users, con);
            return response;
        }
    }
}
