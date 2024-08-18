using FilmesAPI.Data.Dtos;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private CinemaServices _cinemaServices;

        public CinemaController(CinemaServices cinemaServices)
        {
            _cinemaServices = cinemaServices;
        }
  

        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            ReadCinemaDto readDto = _cinemaServices.AdicionaCinema(cinemaDto);
            return CreatedAtAction(nameof(RecuperaCinemaPorId), new { Id = readDto.Id }, readDto);
        }

        [HttpGet]
        public IActionResult RecuperaCinemas([FromQuery] string? nomeDoFilme = null)
        {
            List<ReadCinemaDto> cinemasDto = _cinemaServices.RecuperaCinemas(nomeDoFilme);
            return Ok(cinemasDto);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaCinemaPorId(int id)
        {
            ReadCinemaDto cinemaDto = _cinemaServices.RecuperaCinemaPorId(id);

            if(cinemaDto is not null)
                return Ok(cinemaDto);

            return NotFound("Cinema não encontrado.");
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            Result resultado = _cinemaServices.AtualizaCinema(id, cinemaDto);
            if(resultado.IsFailed)
                return NotFound();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletaCinema(int id)
        {
            Result resultado = _cinemaServices.DeletaCinema(id);
            if(resultado.IsFailed)
                return NotFound();
            return NoContent();
        }

    }
}
