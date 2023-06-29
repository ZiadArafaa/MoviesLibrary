using MoviesLibrary.Core.Dtos;
using MoviesLibrary.Core.Models;

namespace MoviesLibrary.Core.Repositories
{
    public interface IAuthorRepository : IBaseRepository<ApplicationUser>
    {
        public Task<string?> RegisterAsync(ApplicationUser user, string password);
        public Task<AuthDto> LoginAync(string email, string password);
        public Task LogoutAsync(ApplicationUser user);
        public Task<ApplicationUser?> IsExistAsync(string email);
    }

}
