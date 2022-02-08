using Assignment_3.models;
using Assignment_3.models.Domain;
using Assignment_3.models.DTO.Character;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_3.services
{
    public  class CharacterService : ICharacterService
    {
        private readonly MovieCharactersDbContext _context;
        private readonly IMapper _mapper;
        public CharacterService(MovieCharactersDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets all characters.
        /// </summary>
        /// <returns></returns>
        public  async Task<IEnumerable<Character>> GetCharacters()
        {
            return await _context.Characters.ToListAsync();
        }

        /// <summary>
        /// gets specific character.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Character> GetCharacters(int id)
        {
            return await _context.Characters.FindAsync(id);
        }

        /// <summary>
        /// Updates a character.
        /// </summary>
        /// <param name="character"></param>
        public void PutCharacters(CharacterDTO character)
        {
            Character ch = _mapper.Map<Character>(character);
            _context.Entry(ch).State = EntityState.Modified;
        }
        /// <summary>
        /// Updates a character.
        /// </summary>
        /// <param name="character"></param>
        public void DeleteCharacters(Character character)
        {
            _context.Characters.Remove(character);
           
        }



        public bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }
    }
}
