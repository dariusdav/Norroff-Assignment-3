﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment_3.models;
using Assignment_3.models.Domain;
using AutoMapper;
using Assignment_3.Models.DTO.Movie;
using Assignment_3.models.DTO.Character;

namespace Assignment_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MoviesController : ControllerBase
    {
        private readonly MovieCharactersDbContext _context;
        private readonly IMapper _mapper;

        public MoviesController(MovieCharactersDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all the Movies from the database.
        /// </summary>
        /// <returns></returns>
        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            return _mapper.Map<List<MovieDTO>>(await _context.Movies.ToListAsync());
        }
        /// <summary>
        /// Gets a specific movie from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetMovie(int id)
        {
            var movie = _mapper.Map<MovieDTO>(await _context.Movies
                .Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id));

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }
        /// <summary>
        /// Gets all characters from a movie with a given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<List<CharacterDTO>>> GetCharactersByMovieId(int id)
        {
            List<CharacterDTO> characterDTOs = _mapper.Map<List<CharacterDTO>>(await _context.Movies
                .Where(m => m.Id == id)
                .SelectMany(m => m.Characters)
                .Select(c => c).ToListAsync());
            if (await _context.Movies.FindAsync(id) == null)
            {
                return NotFound();
            }
            return characterDTOs;
        }
        /// <summary>
        /// Updates a particular movie from database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movie"></param>
        /// <returns></returns>
        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieDTO movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(_mapper.Map<Movie>(movie)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        /// <summary>
        /// Updates a movie with characters with a specified array of character Id's.
        /// </summary>
        /// <param name="id">Id of the movie</param>
        /// <param name="characterIds">array of character Id's</param>
        /// <returns></returns>
        [HttpPut("{id}/characters")]
        public async Task<IActionResult> PutCharactersIntoMovie(int id, int[] characterIds)
        {
            Movie movie = _context.Movies.Find(id);
            if (id != movie.Id)
            {
                return BadRequest();
            }
            List<Character> cList = new();
            for (int i = 0; i < characterIds.Length; i++)
            {
               cList.Add(_context.Characters.Where(c => c.Id == characterIds[i]).First());
            }
            movie.Characters = cList;     
            _context.Entry(_mapper.Map<Movie>(movie)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        /// <summary>
        /// Creates a new Movie into the database.
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<MovieDTO>> PostMovie(MovieCreateDTO movie)
        {
            Movie  m = _context.Movies.Add(_mapper.Map<Movie>(movie)).Entity;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = m.Id }, movie);
        }
        /// <summary>
        /// Deletes a movie from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
