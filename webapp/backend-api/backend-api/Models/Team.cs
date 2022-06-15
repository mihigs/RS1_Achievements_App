using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Models
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
        public List<TeamAchievements> AvailableAchievements { get; set; }
    }
}
