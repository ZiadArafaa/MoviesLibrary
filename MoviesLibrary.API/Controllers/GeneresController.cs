using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesLibrary.Core;
using MoviesLibrary.Core.Consts;
using MoviesLibrary.Core.Dtos;
using MoviesLibrary.Core.Models;
using MoviesLibrary.Core.Repositories;
using MoviesLibrary.Core.Services;
using MoviesLibrary.EF;
using UoN.ExpressiveAnnotations.NetCore.Analysis;

namespace MoviesLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AppRole.Admin)]
    public class GeneresController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GeneresController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateGenereDto dto)
        {
            var model = new Genere { Name = dto.Name };
            var Poster = await ImageService.CreateImageAsync(dto.Poster);

            if (Poster is null)
                return BadRequest("Check size or extensions.");

            model.Poster = Poster;

            await _unitOfWork.Generes.CreateAsync(model);
            _unitOfWork.SaveChanges();

            return Ok(model);
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, [FromForm] UpdateGenereDto dto)
        {
            var model = await _unitOfWork.Generes.GetByIdAsync(id);
            if (model is null)
                return NotFound();

            model.Name = dto.Name;
            if (dto.Poster is not null)
            {
                var Poster = await ImageService.CreateImageAsync(dto.Poster);

                if (Poster is null)
                    return BadRequest("Check size or extensions.");

                model.Poster = Poster;
            }

            _unitOfWork.Generes.Update(model);
            _unitOfWork.SaveChanges();

            return Ok(model);
        }
        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var Generes = await _unitOfWork.Generes.GetAllAsync();

            if (!Generes.Any())
                return NotFound();

            return Ok(Generes);
        }
        [AllowAnonymous]
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetByIdAsync(byte id)
        {
            var Genere = await _unitOfWork.Generes.GetByIdAsync(id);

            if (Genere is null)
                return NotFound();

            return Ok(Genere);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var Genere = await _unitOfWork.Generes.GetByIdAsync(id);

            if (Genere is null)
                return NotFound();

            _unitOfWork.Generes.Delete(Genere);
            _unitOfWork.SaveChanges();

            return Ok(Genere);
        }
    }
}
