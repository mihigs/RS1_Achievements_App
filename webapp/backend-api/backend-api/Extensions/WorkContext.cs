using backend_api.Data;
using backend_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Extensions
{
    public class WorkContext : IWorkContext
    {
        private User _currentUser;
        private readonly HttpContext _httpContext;
        private readonly IRepository<User> _userRepository;

        public WorkContext(UserManager<User> userManager,
                           IRepository<User> userRepository,
                           IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _httpContext = contextAccessor.HttpContext;
        }
        public async Task<User> GetCurrentUserAsync()
        {
            if (_currentUser != null)
            {
                return _currentUser;
            }
            var userId = GetCurrentUserId();
            var user = _userRepository.Query().FirstOrDefault(r => r.Id == userId);

            _currentUser = user;

            return _currentUser;
        }

        public string GetCurrentUserId()
        {
            var userId = _httpContext.User.Claims.FirstOrDefault(x => x.Type == "uid")?.Value;
            return userId;
        }
    }
}
