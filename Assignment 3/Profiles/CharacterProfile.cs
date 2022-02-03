using Assignment_3.models.Domain;
using Assignment_3.models.DTO.Character;
using Assignment_3.Models.DTO.Character;
using AutoMapper;

namespace Assignment_3.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterDTO>().ReverseMap();
            CreateMap<Character, CharacterCreateDTO>().ReverseMap();
        }
    }
}
