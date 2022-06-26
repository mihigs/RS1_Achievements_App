using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Models
{
    public class TeamAchievements : BaseEntity
    {
        public long TeamId { get; set; }
        public Team Team { get; set; }
        public long AchievementId { get; set; }
        public Achievement Achievement { get; set; }
    }
}
