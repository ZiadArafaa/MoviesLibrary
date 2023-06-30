using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesLibrary.Core.Dtos;
using MoviesLibrary.Core.Models;
using MoviesLibrary.Core.Repositories;
using MoviesLibrary.Core.Services;
using UoN.ExpressiveAnnotations.NetCore.Analysis;

namespace MoviesLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneresController : ControllerBase
    {
        private readonly IGenereRepository _repository;
        private readonly IMapper _mapper;
        public GeneresController(IGenereRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateGenereDto dto)
        {
            var model = new Genere { Name = dto.Name };
            var Poster = await ImageService.CreateImageAsync(dto.Poster);

            if (Poster is null)
                return BadRequest("Check size or extensions.");

            model.Poster = Poster;

            return Ok(await _repository.CreateAsync(model));
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, [FromForm] UpdateGenereDto dto)
        {
            var model = await _repository.GetByIdAsync(id);
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

            int effected = _repository.Update(model);

            if (!effected.Equals(1))
                return BadRequest("Something Wrong!");

            return Ok(model);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var Generes = await _repository.GetAllAsync();

            if (!Generes.Any())
                return NotFound();

            return Ok(Generes);
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetByIdAsync(byte id)
        {
            var Genere = await _repository.GetByIdAsync(id);

            if (Genere is null)
                return NotFound();

            return Ok(Genere);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var Genere = await _repository.GetByIdAsync(id);

            if (Genere is null)
                return NotFound();

            int effected = _repository.Delete(Genere);

            if(!effected.Equals(1))
                return BadRequest("Something Wrong!");

            return Ok(Genere);
        }
    }
}
