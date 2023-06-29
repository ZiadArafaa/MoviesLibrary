using MoviesLibrary.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.Core.Dtos
{
    public class RegisterDto
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        //To Do:Move Error message to class
        [MaxLength(10,ErrorMessage = ErrorMessage.MaxLength)]
        public string UserName { get; set; } = null!;
        //To Do:Move Error message to class
        [DataType(DataType.Password), StringLength(30, MinimumLength = 8, ErrorMessage = ErrorMessage.StringLength)]
        public string Password { get; set; } = null!;
        [DataType(DataType.Password), StringLength(30, MinimumLength = 8)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
        //To Do:Move Error message to class
        [RegularExpression("01[0125][0-9]{8}", ErrorMessage = ErrorMessage.RegexPhone)]
        public string PhoneNumber { get; set; } = null!;

    }
}
