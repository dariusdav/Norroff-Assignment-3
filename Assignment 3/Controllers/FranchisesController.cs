using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment_3.models;
using Assignment_3.models.Domain;
using AutoMapper;
using Assignment_3.models.DTO.Franchise;
using Assignment_3.Models.DTO.Movie;
using Assignment_3.Models.DTO.Franchise;
using Assignment_3.models.DTO.Character;
using Assignment_3.services;

namespace Assignment_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FranchisesController : ControllerBase
    {
        private readonly MovieCharactersDbContext _context;
        private readonly IMapper _mapper;
        private readonly FranchiseService _franchiseService;
        public FranchisesController(MovieCharactersDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _franchiseService = new(_context, _mapper);
            
        }

        // GET: api/Franchises
        /// <summary>
        /// Gets all the franchises in the database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseMovieDTO>>> GetFranchises()
        {

            return _mapper.Map<List<FranchiseMovieDTO>>(await _franchiseService.GetFranchises());
        }

        /// <summary>
        /// Gets a franchise with specified id from the database.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseMovieDTO>> GetFranchiseId(int id)
        {
            var franchise = await _franchiseService.GetFranchise(id);
            if (franchise == null)
            {
                return NotFound();
            }
            return franchise;
        }
        /// <summary>
        /// Gets all the movies from a franchise with a given id from the database.
        /// </summary>
        /// <param name="id">Id of the franchise</param>
        /// <returns> returns Movies in JSON format</returns>
        // GET: api/Franchises/5
        [HttpGet("{id}/movies")]
        public async Task<ActionResult<List<MovieDTO>>> GetFranchise(int id)
        {
            var movies = await _franchiseService.GetFranchiseMovies(id);

            if (movies == null)
            {
                return NotFound();
            }

            return movies;
        }

        /// <summary>
        /// Gets all the characters from a franchise with a given id from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<List<CharacterDTO>>> GetFranchiseCharacters(int id)
        {
            var characters = await _franchiseService.GetFranchiseCharacters(id);
            if (characters == null)
            {
                return NotFound();
            }

            return characters;
        }
        /// <summary>
        /// Updates a franchise with movies with a specified array of character Id's.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="moviesIds"></param>
        /// <returns></returns>
        [HttpPut("{id}/movies")]
        public async Task<IActionResult> PutMoviesFranchise(int id, int[] moviesIds)
        {
            FranchiseDTO franchise = _context.Franchises.Find(id);
            if (id != franchise.Id)
            {
                return BadRequest();
            }
            _franchiseService.PutMoviesFranchise(franchise, moviesIds);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FranchiseExists(id))
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

        // PUT: api/Franchises/5
        /// <summary>
        /// Updates a franchise with given Id.
        /// </summary>
        /// <param name="id"> </param>
        /// <param name="franchise"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchiseMovieDTO franchise)
        {
            if (id != franchise.Id)
            {
                return BadRequest();
            }

            _franchiseService.PutFranchise(franchise);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FranchiseExists(id))
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

        // POST: api/Franchises
        /// <summary>
        /// Adds a new franchise into the database.
        /// </summary>
        /// <param name="franchise"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<FranchiseDTO>> PostFranchise(FranchiseCreateDTO franchise)
        {
            FranchiseDTO f = _franchiseService.PostFranchise(franchise);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFranchise", new { id = f.Id }, franchise);
        }
        /// <summary>
        /// Deletes a franchise from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Franchises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise == null)
            {
                return NotFound();
            }
            _franchiseService.DeleteFranchise(id,franchise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FranchiseExists(int id)
        {
            return _context.Franchises.Any(e => e.Id == id);
        }
    }
}
