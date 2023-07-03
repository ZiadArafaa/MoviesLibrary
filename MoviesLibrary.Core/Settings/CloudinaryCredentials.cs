using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.Core.Settings
{
    public class CloudinaryCredentials
    {
        public string CloudName { get; set; } = null!;
        public string APIKey { get; set; } = null!;
        public string APISecret { get; set; } = null!;
    }
}
