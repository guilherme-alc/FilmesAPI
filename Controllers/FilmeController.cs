using FilmesAPI.Data.Dtos;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeServices _filmeServices;
        public FilmeController(FilmeServices filmeServices)
        {
            _filmeServices = filmeServices;
        }

        [HttpPost]
        public IActionResult  CreateFilme ([FromBody] CreateFilmeDto filmeDto)
        {
            ReadFilmeDto readDto = _filmeServices.AdicionaFilme(filmeDto);
            return CreatedAtAction(nameof(GetByIdFilme), new { Id = readDto.Id }, readDto);
        }
        [HttpGet]
        public IActionResult GetFilmes([FromQuery] int? duracao = null)
        {
            List <ReadFilmeDto> filmesDto = _filmeServices.RecuperaFilmes(duracao);
            return Ok(filmesDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdFilme(int id) 
        {
            ReadFilmeDto readDto = _filmeServices.RecuperarFilmePorId(id);

            if (readDto is null)
                return NotFound("Filme não encontrado.");

            return Ok(readDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Result resultado = _filmeServices.AtualizaFilme(id, filmeDto);

            if(resultado.IsFailed)
                return NotFound("Filme não encontrado.");

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteFilme(int id)
        {
            Result resultado = _filmeServices.DeletaFilme(id);

            if (resultado.IsFailed)
                return NotFound("Filme não encontrado."); 

            return NoContent();
        }
    }
}