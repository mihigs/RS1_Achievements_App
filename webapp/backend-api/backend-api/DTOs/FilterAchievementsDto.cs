using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.DTOs
{
    public class FilterAchievementsDto
    {
        public string UserId { get; set; }
        public long? TeamId { get; set; }
        public long? EventId { get; set; }
    }
}
