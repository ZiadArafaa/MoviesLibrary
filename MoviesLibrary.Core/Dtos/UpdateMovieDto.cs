using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.Core.Dtos
{
    public class UpdateMovieDto : MovieDto
    {
        public IFormFile? Poster { get; set; }
    }
}
