using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Models
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset Modified { get; set; } = DateTimeOffset.Now;
    }
}
