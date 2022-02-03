using Assignment_3.models.Domain;
using Assignment_3.Models.DTO.Movie;
using AutoMapper;
using System.Linq;

namespace Assignment_3.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie,MovieDTO>()
                .ForMember(mdto => mdto.CharactersId,
                opt => opt.MapFrom( m => m.Characters
                .Select( c => c.Id).ToList())).ReverseMap();
            CreateMap<Movie,MovieCreateDTO>().ReverseMap();
        }
    }
}
