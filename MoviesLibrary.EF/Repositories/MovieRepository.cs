using MoviesLibrary.Core.Models;
using MoviesLibrary.Core.Repositories;
using MoviesLibrary.EF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.EF.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext context):base(context)
        {
        }
    }
}
