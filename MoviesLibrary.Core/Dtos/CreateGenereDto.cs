using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.Core.Dtos
{
    public class CreateGenereDto
    {
        [MaxLength(50,ErrorMessage ="{0} Maxmuim chars is {1}.")]
        public string Name { get; set; } = null!;
        public IFormFile Poster { get; set; } = null!;
    }
}