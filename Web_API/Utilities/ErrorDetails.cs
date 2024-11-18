using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web_API.Utilities
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public List<ErrorDetails> Details { get; set; } = new List<ErrorDetails>();

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class ErrorDetails
    {
        public string? Title { get; set; }
        public string? Message { get; set; }
    }

   

}
