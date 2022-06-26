using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Models
{
    public class UserEvents : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public long EventId { get; set; }
        public Event Event{ get; set; }
    }
}
