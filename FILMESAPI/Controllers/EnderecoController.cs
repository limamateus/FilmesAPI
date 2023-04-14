using AutoMapper;
using FilmesAPI.Dtos;
using FilmesAPI.Models;
using FILMESAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {

        private FilmeContext _context;
        private IMapper _mapper;

        public EnderecoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPost]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarEnderecoPorId), new { Id = endereco.Id }, endereco);

        }
        [HttpGet]
        public IEnumerable<ReadEnderecoDto> RecuperarEnderecoPorId()
        {
            return _mapper.Map<List<ReadEnderecoDto>>(_context.Enderecos.ToList());
        }

        [HttpPut("{id}")]
        public IActionResult AtualzarEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            var endereco = _context.Filmes.FirstOrDefault(cinema => cinema.Id == id);

            if (enderecoDto == null) return NotFound();

            _mapper.Map(enderecoDto, endereco);
            _context.SaveChanges();

            return NoContent();


        }


        [HttpDelete("{id}")]

        public IActionResult DeletaEndereco(int id)
        {
            var endereco = _context.Filmes.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null) return NotFound();

            _context.Remove(endereco);
            _context.SaveChanges();

            return NoContent();

        }

    }
}
