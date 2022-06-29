using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Models
{
    public class Achievement : BaseEntity
    {
        //TODO: add image
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public long? EventId { get; set; }
        public Event Event { get; set; }
        [JsonIgnore]
        public long? TeamId { get; set; }
        public Team Team { get; set; }
        public string CreatedBy { get; set; }
        public string Tier { get; set; }
        public string IconUrl { get; set; }
        public List<User> AchievedBy { get; set; }

    }
}
