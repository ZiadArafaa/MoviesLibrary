using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.Core.Models
{
    public class ApplicationUser :IdentityUser
    {
        public bool Status { get; set; }
    }
}
