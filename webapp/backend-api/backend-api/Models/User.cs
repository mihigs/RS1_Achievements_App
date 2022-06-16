using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace backend_api.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<TeamMember> Teams { get; set; }
        public List<AchievementsUser> Achievements { get; set; }
        public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset Modified { get; set; } = DateTimeOffset.Now;
        public List<RefreshToken> RefreshTokens { get; set; }

    }
}
