﻿using AutoMapper;
using FilmesAPI.Dtos;
using FILMESAPI.Data;
using FILMESAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FILMESAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {

        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionarFilmes([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();

            return CreatedAtAction(nameof(RecuperarFilmes),
                                  new { id = filme.Id }, filme);

        }


        [HttpGet]
        public IEnumerable<ReadFilmeDto> RecuperarFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take)); // skip e take é usando para paginação sim  onde eu informo o 1 e o fim 
        }
       
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult RecuperaFilmePorId(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null) return NotFound();

             var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
            return Ok(filmeDto);

        }
        /// <summary>
        /// Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPut("{id}")]
        public IActionResult AtualzarFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null) return NotFound();

            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();

            return NoContent();


        }


        [HttpPatch("{id}")]
        public IActionResult AtualzarFilmeParcial(int id, [FromBody] JsonPatchDocument<UpdateFilmeDto> patch)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null) return NotFound();

            var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

           
            try
            {
                patch.ApplyTo(filmeParaAtualizar, ModelState);
                if (!TryValidateModel(filmeParaAtualizar))
                {
                    return ValidationProblem(ModelState);
                }
            }
            catch (Exception e)
            {

               return BadRequest(e.Message);
            }
          
            _mapper.Map(filmeParaAtualizar, filme);
            _context.SaveChanges();

            return NoContent();


        }



        [HttpDelete("{id}")]

        public IActionResult DeletaFilme(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if(filme == null) return NotFound();

            _context.Remove(filme);
            _context.SaveChanges();

            return NoContent();

        }
    }
}
