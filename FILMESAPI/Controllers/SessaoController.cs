using AutoMapper;
using FilmesAPI.Dtos;
using FilmesAPI.Models;
using FILMESAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController: ControllerBase
    {
        private IMapper _mapper;

        private FilmeContext _context;

        public SessaoController(IMapper mapper, FilmeContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        [HttpPost]
        public IActionResult AdicionarSessao([FromBody] CreateSessaoDto sessaoDto)
        {
            Sessao sessao = _mapper.Map<Sessao>(sessaoDto);


            _context.Sessao.Add(sessao);
            _context.SaveChanges();

            return CreatedAtAction(nameof(RecuperarSessoes),
                                  new { filmeId = sessao.FilmeId , cinemaId = sessao.CinemaId }, sessao);

        }


        [HttpGet]
        public IEnumerable<ReadSessaoDto> RecuperarSessoes([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _mapper.Map<List<ReadSessaoDto>>(_context.Sessao.Skip(skip).Take(take)); // skip e take é usando para paginação sim  onde eu informo o 1 e o fim 
        }

        [HttpGet("{filmeId}/{cinemaId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult RecuperaSessaoPorId(int filmeId, int cinemaId)
        {
            var sessao = _context.Sessao.FirstOrDefault(sessao => sessao.FilmeId == filmeId && sessao.CinemaId == cinemaId);

            if (sessao == null) return NotFound();

            var sessaoDto = _mapper.Map<ReadFilmeDto>(sessao);
            return Ok(sessaoDto);

        }
       
        //[HttpPut("{id}")]
        //public IActionResult AtualzarSessao(int id, [FromBody] filmeDto)
        //{
        //    var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        //    if (filme == null) return NotFound();

        //    _mapper.Map(filmeDto, filme);
        //    _context.SaveChanges();

        //    return NoContent();


        //}





        [HttpDelete("{filmeId}/{cinemaId}")]

        public IActionResult DeletaSessao(int filmeId, int cinemaId)
        {
            var sessao = _context.Sessao.FirstOrDefault(sessao => sessao.FilmeId == filmeId && sessao.CinemaId == cinemaId);
            if (sessao == null) return NotFound();

            _context.Remove(sessao);
            _context.SaveChanges();

            return NoContent();

        }
    }
}
