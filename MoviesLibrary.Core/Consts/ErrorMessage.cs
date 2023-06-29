using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.Core.Consts
{
    public static class ErrorMessage
    {
        public const string MaxLength = "{0} mustn't greater than {1} char.";
        public const string StringLength = "{0} must be from {2} to {1} char.";
        public const string RegexPhone = "Phone isn't valid.";
    }
}
