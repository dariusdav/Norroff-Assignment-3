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

namespace Assignment_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FranchisesController : ControllerBase
    {
        private readonly MovieCharactersDbContext _context;
        private readonly IMapper _mapper;

        public FranchisesController(MovieCharactersDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Franchises
        /// <summary>
        /// API call to retrieve all the Franchises in the database 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseBaseDTO>>> GetFranchises()
        {

            return _mapper.Map<List<FranchiseBaseDTO>>(await _context.Franchises.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<FranchiseBaseDTO>>> GetFranchiseId()
        {

            return _mapper.Map<List<FranchiseBaseDTO>>(await _context.Franchises.ToListAsync());
        }
        /// <summary>
        /// A call to retrieve all the movies from a specific Franchise 
        /// </summary>
        /// <param name="id">Id of the franchise</param>
        /// <returns> returns Movies in JSON format</returns>
        // GET: api/Franchises/5
        [HttpGet("{id}/movies")]
        public async Task<ActionResult<List<MovieDTO>>> GetFranchise(int id)
        {
            List<MovieDTO> movies = _mapper.Map<List<MovieDTO>>(await _context.Movies
                .Where(m => m.FranchiseId == id).ToListAsync());
            var franchise = await _context.Franchises.FindAsync(id);
            //franchise.Movies = movies;
            if (franchise == null)
            {
                return NotFound();
            }

            return movies;
        }

        /// <summary>
        /// Call to get all the characters in a franchise.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<List<CharacterDTO>>> GetFranchiseCharacters(int id)
        {
            // First getting all the movies in the list
            List<int> characterID = await _context.Movies
                .Where(m => m.FranchiseId == id)
                .SelectMany(m => m.Characters)
                .Select(c => c.Id)
                .ToListAsync();
            List<CharacterDTO> characters = await _context.Characters
                .Where(c => characterID.Contains(c.Id))
                .Select(c => _mapper.Map<CharacterDTO>(c)).ToListAsync();
            var franchise = await _context.Franchises.FindAsync(id);
            //franchise.Movies = movies;
            if (franchise == null)
            {
                return NotFound();
            }

            return characters;
        }

        // PUT: api/Franchises/5
        /// <summary>
        /// Updates a franchise.
        /// </summary>
        /// <param name="id"> </param>
        /// <param name="character"></param>
        /// <returns> A response of operations sucess</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchiseBaseDTO franchise)
        {
            if (id != franchise.Id)
            {
                return BadRequest();
            }
            
            _context.Entry(_mapper.Map<Franchise>(franchise)).State = EntityState.Modified;

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
        public async Task<ActionResult<Franchise>> PostFranchise(FranchiseBaseDTO franchise)
        {
            _context.Franchises.Add(_mapper.Map<Franchise>(franchise));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFranchise", new { id = franchise.Id }, franchise);
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

            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FranchiseExists(int id)
        {
            return _context.Franchises.Any(e => e.Id == id);
        }
    }
}
