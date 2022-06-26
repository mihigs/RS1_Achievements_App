using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.DTOs
{
    public class CreateAchievementDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public long? EventId { get; set; }
        public long? TeamId { get; set; }
    }
}
