using Assignment_3.models.Domain;
using Assignment_3.models.DTO.Character;
using AutoMapper;

namespace Assignment_3.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterDTO>();
        }
    }
}
