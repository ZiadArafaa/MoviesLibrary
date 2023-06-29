using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesLibrary.Core.Dtos;
using MoviesLibrary.Core.Models;
using MoviesLibrary.Core.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MoviesLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;
        public AuthorsController(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> CreateUserAsync([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(dto);

            if (await _repository.IsExistAsync(dto.Email) is not null)
                return BadRequest("Sorry, email exist!");

            var ErrorResult = await _repository.RegisterAsync(_mapper.Map<ApplicationUser>(dto), dto.Password);

            if (!string.IsNullOrEmpty(ErrorResult))
                return BadRequest(ErrorResult);

            return Ok();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
        {
            var Result = await _repository.LoginAync(dto.Email, dto.Password);

            if (!Result.IsAuthenticated)
                return NotFound(Result.Message);

            return Ok(Result);
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutAsync([EmailAddress] string Email)
        {
            var User = await _repository.IsExistAsync(Email);
            if (User is null)
                return NotFound();

            await _repository.LogoutAsync(User);
            return Ok();
        }
    }
}
