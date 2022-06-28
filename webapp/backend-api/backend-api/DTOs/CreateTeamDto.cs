using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.DTOs
{
    public class CreateTeamDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string TeamIconUrl { get; set; }
    }
}
