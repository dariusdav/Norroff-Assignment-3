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
using Assignment_3.models.DTO.Character;

namespace Assignment_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CharactersController : ControllerBase
    {
        private readonly MovieCharactersDbContext _context;
        private readonly IMapper _mapper;

        public CharactersController(MovieCharactersDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Characters
        /// <summary>
        /// API call to retrieve all the characters in the database
        /// </summary>
        /// <returns>returns a list of Characters in JSON format</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetCharacters()
        {
            return _mapper.Map<List<CharacterDTO>>(await _context.Characters.ToListAsync());
        }

        // GET: api/Characters/5
        /// <summary>
        /// A call to retrieve a specific Character with its id from the database
        /// </summary>
        /// <param name="id">The Id for the character to be provided</param>
        /// <returns> Character in JSON format</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDTO>> GetCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);

            if (character == null)
            {
                return NotFound();
            }

            return _mapper.Map<CharacterDTO>(character);
        }

        // PUT: api/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Idenpotent call to update a character with given Id.
        /// </summary>
        /// <param name="id"> </param>
        /// <param name="character"></param>
        /// <returns> A response of whether the character that should be updated exist and the success 
        /// of the operation</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, CharacterDTO character)
        {
            if (id != character.Id)
            {
                return BadRequest();
            }
            Character ch = _mapper.Map<Character>(character);
            _context.Entry(ch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
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

        // POST: api/Characters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// API call to add a new character into the database
        /// </summary>
        /// <param name="character"></param>
        /// <returns>API call response</returns>
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(CharacterDTO character)
        {
            _context.Characters.Add(_mapper.Map<Character>(character));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacter", new { id = character.Id }, character);
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }
    }
}
