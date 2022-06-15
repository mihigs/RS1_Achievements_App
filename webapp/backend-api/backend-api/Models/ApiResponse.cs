using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace backend_api.Models
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public IList<string> Errors { get; set; } = new List<string>();
        public dynamic Result { get; set; }
    }
}
