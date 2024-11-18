using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class JwtConfiguration
    {
        public string? ValidIssuer { get; set; }
        public string? ValidAudience { get; set; }
        public string? Secret { get; set; }
    }
}
