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
        //[HttpPut("Update/{id}")]
        //public async Task<IActionResult> UpdateAsync(byte id,[FromForm] UpdateGenereDto dto)
        //{
            
        //}
    }
}
