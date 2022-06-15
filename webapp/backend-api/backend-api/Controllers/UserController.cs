using backend_api.Data;
using backend_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace backend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<User> _userRepository;

        public UserController(
            IRepository<User> userRepository, 
            UserManager<User> userManager
            )
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        /// <summary>
        /// Will return a list of all users in the database.
        /// </summary>
        [HttpGet("list")]
        public IActionResult GetUsers()
        {
            var response = new ApiResponse();
            
            try
            {
                var users = _userRepository.GetAll();
                response.Result = users;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
            return Ok(response);
        }
        /// <summary>
        /// Will return all details related to a selected user.
        /// </summary>
        /// <param name="id">id of the user</param>
        /// <returns>Selected user</returns>
        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            var response = new ApiResponse();

            try
            {
                var user = _userRepository.GetById(id);
                response.Result = user;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
            return Ok(response);
        }
    }
}
