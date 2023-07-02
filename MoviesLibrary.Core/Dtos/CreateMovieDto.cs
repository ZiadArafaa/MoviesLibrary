using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.Core.Dtos
{
    public class CreateMovieDto : MovieDto
    {
        public string PosterUrl { get; set; } = null!;
        public string PublicId { get; set; } = null!;
    }
}
