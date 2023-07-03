using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoviesLibrary.Core;
using MoviesLibrary.Core.Dtos;
using MoviesLibrary.Core.Models;
using MoviesLibrary.Core.Services;
using MoviesLibrary.Core.Settings;

namespace MoviesLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CloudinaryCredentials credentials;
        private readonly Cloudinary cloudinary;
        public MoviesController(IUnitOfWork unitOfWork, IMapper mapper, IOptions<CloudinaryCredentials> options)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            credentials = options.Value;

            Account account = new Account(credentials.CloudName, credentials.APIKey, credentials.APISecret);

            cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateMovieDto dto)
        {
            if ((await _unitOfWork.Generes.GetByIdAsync(dto.GenereId) is null))
                return NotFound("Genere NotFound");

            var model = _mapper.Map<Movie>(dto);
            var Image = await ImageService.UploadImageAsync(dto.Poster, cloudinary);

            if (Image is null)
                return BadRequest("Check image size or extensions.");

            model.PosterUrl = Image["Url"];
            model.PublicId = Image["PublicId"];

            await _unitOfWork.Movies.CreateAsync(model);
            _unitOfWork.SaveChanges();

            return Ok(model);
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] UpdateMovieDto dto)
        {
            var model = await _unitOfWork.Movies.GetByIdAsync(id);
            if (model is null)
                return NotFound("Not found movie.");

            if ((await _unitOfWork.Generes.GetByIdAsync(dto.GenereId)) is null)
                return NotFound("Not found genere.");

            if(dto.Poster is not null)
            {
                var ImageResult = await ImageService.UploadImageAsync(dto.Poster, cloudinary);
                if (ImageResult is null)
                    return BadRequest("Check image size or extensions.");

                await ImageService.DeleteAsync(model.PublicId, cloudinary);

                model.PosterUrl = ImageResult["Url"];
                model.PublicId = ImageResult["PublicId"];
            }

            _unitOfWork.Movies.Update(_mapper.Map(dto, model));
            _unitOfWork.SaveChanges();

            return Ok(model);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var models = await _unitOfWork.Movies.GetAllAsync();

            if (!models.Any())
                return NotFound();

            return Ok(models);
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var model = await _unitOfWork.Movies.GetByIncludeAsync(m => m.Id == id, new[] {"Genere"});
            if (model is null) return NotFound();

            return Ok(model);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _unitOfWork.Movies.GetByIdAsync(id);
            if (model is null)
                return NotFound();

            await ImageService.DeleteAsync(model.PublicId, cloudinary);

            _unitOfWork.Movies.Delete(model);
            _unitOfWork.SaveChanges();

            return Ok(model);
        }
    }
}
