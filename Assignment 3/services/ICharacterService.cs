using Assignment_3.models;
using Assignment_3.models.Domain;
using Assignment_3.models.DTO.Character;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment_3.services
{
    public interface ICharacterService
    {
        public  Task<IEnumerable<Character>> GetCharacters();
        public Task<Character> GetCharacters(int id);
        public void PutCharacters(CharacterDTO character);
        public  void DeleteCharacters(Character character);
        public bool CharacterExists(int id);
    }
}
