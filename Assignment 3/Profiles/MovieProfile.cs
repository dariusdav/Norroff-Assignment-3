using Assignment_3.models.Domain;
using Assignment_3.Models.DTO.Movie;
using AutoMapper;

namespace Assignment_3.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie,MovieDTO>();
        }
    }
}
