using MoviesLibrary.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.Core.Dtos
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password), StringLength(30, MinimumLength = 8, ErrorMessage = ErrorMessage.StringLength)]
        public string Password { get; set; } = null!;
    }
}
