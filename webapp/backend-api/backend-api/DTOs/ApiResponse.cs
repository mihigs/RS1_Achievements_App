using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace backend_api.DTOs
{
    /// <summary>
    ///
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        ///
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public IList<ApiError> Errors { get; set; } = new List<ApiError>();
        /// <summary>
        ///
        /// </summary>
        public dynamic Result { get; set; }
    }
}
