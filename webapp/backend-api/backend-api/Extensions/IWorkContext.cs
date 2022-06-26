using backend_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Extensions
{
    public interface IWorkContext
    {
        Task<User> GetCurrentUserAsync();
        string GetCurrentUserId();
    }
}
