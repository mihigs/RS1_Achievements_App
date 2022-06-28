using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Models
{
    public class TeamMember : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public long TeamId { get; set; }
        public Team Team{ get; set; }
    }
}
