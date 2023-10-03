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
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]
        public Response register(Users users)
        {
            Response response = new Response();
            UsersRepository userRepository = new UsersRepository();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            response = userRepository.register(users, con);
            return response;
        }

        [HttpPost]
        [Route("login")]
        public Response login(Users users)
        {
            Response response = new Response();
            UsersRepository userRepository = new UsersRepository();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            response = userRepository.login(users, con);
            return response;
        }
        
        [HttpPost]
        [Route("viewUser")]
        public Response viewUsers(Users users)
        {
            UsersRepository ur = new UsersRepository();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = ur.viewUser(users, con);
            return response;
        }
    }
}
