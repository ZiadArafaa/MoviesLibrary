using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MoviesLibrary.Core;
using MoviesLibrary.Core.Models;
using MoviesLibrary.Core.Repositories;
using MoviesLibrary.Core.Settings;
using MoviesLibrary.EF.Data;
using MoviesLibrary.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IAuthorRepository Authors { get; private set; }

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> user, IOptions<JWT> options)
        {
            _context = context;
            Authors = new AuthorRepository(user, options);
        }

        public IGenereRepository Generes => new GenereRepository(_context);
        public IMovieRepository Movies => new MovieRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
