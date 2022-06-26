using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.DTOs
{
    public class CreateEventDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        public string Description { get; set; }
    }
}
