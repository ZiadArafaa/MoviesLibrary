using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.Core.Dtos
{
    public class AuthDto
    {
        public bool IsAuthenticated { get; set; }
        public string? Message { get; set; } 
        public string? Email { get; set; } 
        public string? UserName { get; set; } 
        public string? Token { get; set; }
        public DateTime ExpireOn { get; set; } 
    }
}
