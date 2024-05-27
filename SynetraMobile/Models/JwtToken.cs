using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynetraMobile.Models
{
    public class JwtToken
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
