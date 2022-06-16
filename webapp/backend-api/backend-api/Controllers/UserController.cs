using backend_api.Data;
using backend_api.DTOs;
using backend_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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
                response.Errors.Add(new ApiError("getUser", ex.Message));
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
        [Authorize]
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
                response.Errors.Add(new ApiError("getUser", ex.Message));
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
            return Ok(response);
        }
        [HttpPost("register")]
        public async Task<ApiResponse> RegisterAsync(RegisterDto model)
        {
            var response = new ApiResponse();
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Data.Constants.Authorization.default_registration_role.ToString());
                }
                else if (result.Errors.Any())
                {
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    foreach (var item in result.Errors)
                    {
                        response.Errors.Add(new ApiError("register", item.Description));
                    }
                    return response;
                }

                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Result = $"User Registered with email {user.UserName}";
            }
            else
            {
                response.StatusCode = System.Net.HttpStatusCode.Conflict;
                response.Errors.Add(new ApiError("Duplicate Email", $"Email {user.Email } is already registered."));
            }
            return response;
        }
        [HttpPost("authenticate")]
        public async Task<ActionResult<ApiResponse>> GetTokenAsync(TokenRequestDto model)
        {
            var result = new ApiResponse();

            var authenticationModel = new AuthenticationDto();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                var message = $"No Accounts Registered with {model.Email}.";
                result.StatusCode = System.Net.HttpStatusCode.BadRequest;
                result.Errors.Add(new ApiError("No account", authenticationModel.Message));
                return result;
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authenticationModel.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                authenticationModel.UserId = user.Id;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();

                if (user.RefreshTokens.Any(a => a.IsActive))
                {
                    var activeRefreshToken = user.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                    authenticationModel.RefreshToken = activeRefreshToken.Token;
                    authenticationModel.RefreshTokenExpiration = activeRefreshToken.Expires;
                }
                else
                {
                    var refreshToken = CreateRefreshToken();
                    authenticationModel.RefreshToken = refreshToken.Token;
                    authenticationModel.RefreshTokenExpiration = refreshToken.Expires;
                    user.RefreshTokens.Add(refreshToken);
                    await _userManager.UpdateAsync(user);
                }
                result.StatusCode = System.Net.HttpStatusCode.OK;
                result.Result = authenticationModel;
                return result;
            }
            result.StatusCode = System.Net.HttpStatusCode.BadRequest;
            result.Errors.Add(new ApiError("Credentials", $"Incorrect Credentials for user {user.Email}."));
            return result;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(100),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
        private RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow
                };

            }
        }
    }
}
