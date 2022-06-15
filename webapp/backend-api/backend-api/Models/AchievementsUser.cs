using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Models
{
    public class AchievementsUser : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public long AchievementId { get; set; }
        public Achievement Achievement { get; set; }
    }
}
