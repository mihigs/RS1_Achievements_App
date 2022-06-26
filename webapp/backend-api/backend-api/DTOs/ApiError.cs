using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.DTOs
{
    /// <summary>
    ///
    /// </summary>
    public class ApiError
    {
        public ApiError(string key, string message)
        {
            Key = key;
            Message = message;
        }
        /// <summary>
        ///
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Message { get; set; }
    }
}
