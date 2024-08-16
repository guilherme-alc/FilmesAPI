using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public GerenteController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult AdicionaGerente(CreateGerenteDto gerenteDto)
        {
            Gerente gerente = _mapper.Map<Gerente>(gerenteDto);
            _context.Gerentes.Add(gerente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaGerentePorId), new { Id = gerente.Id }, gerente);
        }
        [HttpGet("{id}")]
        public IActionResult RecuperaGerentePorId(int id) 
        {
            Gerente gerente = _context.Gerentes.Find(id);

            if (gerente is null)
                return NotFound("Gerente não encontrado.");

            ReadGerenteDto gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);

            return Ok(gerenteDto);
        }
    }
}
