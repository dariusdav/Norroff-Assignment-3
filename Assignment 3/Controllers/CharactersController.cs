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
using Assignment_3.Models.DTO.Character;
using Assignment_3.services;

namespace Assignment_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CharactersController : ControllerBase
    {
        private readonly MovieCharactersDbContext _context;
        private readonly IMapper _mapper;
        protected CharacterService _characterService;
        public CharactersController(MovieCharactersDbContext context,IMapper mapper)
        {
            
            _context = context;
            _mapper = mapper;
            _characterService = new(_context, _mapper);
        }

        // GET: api/Characters
        /// <summary>
        /// Gets all the characters in the database.
        /// </summary>
        /// <returns>returns a list of Characters in JSON format</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetCharacters()
        {
            return  _mapper.Map<List<CharacterDTO>>(await _characterService.GetCharacters());
        }

        // GET: api/Characters/5
        /// <summary>
        /// Gets a character with specified id from the database.
        /// </summary>
        /// <param name="id">The Id for the character to be provided</param>
        /// <returns> Character in JSON format</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDTO>> GetCharacter(int id)
        {
            var character = await _characterService.GetCharacters(id);

            if (character == null)
            {
                return NotFound();
            }

            return _mapper.Map<CharacterDTO>(character);
        }

        // PUT: api/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Updates a character with given Id.
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
            _characterService.PutCharacters(character);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_characterService.CharacterExists(id))
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
        /// Adds a new character into the database.
        /// </summary>
        /// <param name="character"></param>
        /// <returns>API call response</returns>
        [HttpPost]
        public async Task<ActionResult<CharacterCreateDTO>> PostCharacter(CharacterCreateDTO character)
        {
            var a = _context.Characters.Add(_mapper.Map<Character>(character));
            Character ch = a.Entity;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacter", new { id = ch.Id }, character);
        }

        /// <summary>
        /// Deletes a character from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            _characterService.DeleteCharacters(character);
            if (character == null)
            {
                return NotFound();
            }
            return NoContent();

        }


    }
}
