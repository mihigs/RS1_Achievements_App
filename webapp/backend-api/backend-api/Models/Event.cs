using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Models
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? EventDate { get; set; }
        public List<UserEvents> Attendees { get; set; }
        public List<EventAchievements> EventAchievements { get; set; }
        public string Description { get; set; }
    }
}
