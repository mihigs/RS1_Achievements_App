using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.DTOs
{
    public class AssignAchievementDto
    {
        public long AchievementId { get; set; }
        public string UserId { get; set; }
    }
}
