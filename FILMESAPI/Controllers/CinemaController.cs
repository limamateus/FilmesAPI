using AutoMapper;
using FilmesAPI.Dtos;
using FilmesAPI.Models;
using FILMESAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public CinemaController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarCinemasPorId), new { Id = cinema.Id }, cinema);

        }
        [HttpGet]
        public IEnumerable<ReadCinemaDto> RecuperarCinemasPorId()
        {
            
            return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());
        }

       [HttpPut("{id}")]
        public IActionResult AtualzarCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            var cinema = _context.Filmes.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema == null) return NotFound();

            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();

            return NoContent();


        }


        [HttpDelete("{id}")]

        public IActionResult DeletaCinema(int id)
        {
            var cinema = _context.Filmes.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null) return NotFound();

            _context.Remove(cinema);
            _context.SaveChanges();

            return NoContent();

        }



    }
}
