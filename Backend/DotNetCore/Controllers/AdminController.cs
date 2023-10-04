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
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("upsertMedicine")]
        public Response upsertMedicine(Medicines medicines)
        {
            MedicinesRepository mr = new MedicinesRepository();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = mr.upsertMedicine(medicines, con);
            return response;
        }

        [HttpGet]
        [Route("userList")]
        public Response userList()
        {
            UsersRepository ur = new UsersRepository();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = ur.userList(con);
            return response;
        }
    }
}
