using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesLibrary.Core;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuthorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> CreateUserAsync([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(dto);

            if (await _unitOfWork.Authors.IsExistAsync(dto.Email) is not null)
                return BadRequest("Sorry, email exist!");

            var ErrorResult = await _unitOfWork.Authors.RegisterAsync(_mapper.Map<ApplicationUser>(dto), dto.Password);

            if (!string.IsNullOrEmpty(ErrorResult))
                return BadRequest(ErrorResult);

            return Ok();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
        {
            var Result = await _unitOfWork.Authors.LoginAync(dto.Email, dto.Password);

            if (!Result.IsAuthenticated)
                return NotFound(Result.Message);

            return Ok(Result);
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutAsync([EmailAddress] string Email)
        {
            var User = await _unitOfWork.Authors.IsExistAsync(Email);
            if (User is null)
                return NotFound();

            await _unitOfWork.Authors.LogoutAsync(User);
            return Ok();
        }
    }
}
