using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utilities
{
    public class CustomException : Exception
    {
        public string? ClassName { get; set; }
        public string? MethodName { get; set; }
        public int? CreationUserId { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public CustomException(string message, Exception? innerException, HttpStatusCode statusCode, string? className = null, string? methodName = null, int? creationUserId = null)
            : base(message, innerException)
        {
            ClassName = className;
            MethodName = methodName;
            CreationUserId = creationUserId;
            StatusCode = statusCode;
        }
    }
}
