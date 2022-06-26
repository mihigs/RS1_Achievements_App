using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Models
{
    public class EventAchievements : BaseEntity
    {
        public long EventId { get; set; }
        public Event Event { get; set; }
        public long AchievementId { get; set; }
        public Achievement Achievement { get; set; }
    }
}
