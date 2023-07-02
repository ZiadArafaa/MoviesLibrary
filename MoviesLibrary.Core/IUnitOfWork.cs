using MoviesLibrary.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.Core
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenereRepository Generes { get; }
        public IMovieRepository Movies { get; }
        public IAuthorRepository Authors { get; }
        public void SaveChanges();
    }
}
