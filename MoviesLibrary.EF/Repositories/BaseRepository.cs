using MoviesLibrary.Core.Repositories;
using MoviesLibrary.EF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext Context;
        public BaseRepository(ApplicationDbContext context)
        {
            Context = context;
        }
    }
}
