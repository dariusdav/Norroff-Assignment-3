using Assignment_3.models.Domain;
using Assignment_3.models.DTO.Franchise;
using Assignment_3.Models.DTO.Franchise;
using Assignment_3.Models.DTO.Movie;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_3.Profiles
{
    public class FranchiseProfile : Profile
    {
            public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseBaseDTO>();
            CreateMap<Franchise, FranchiseMovieDTO>().ForMember(fdto=>fdto.Movies,
                opt => opt.MapFrom(f => f.Movies.Select(m => m.Id).ToList()));
        }
    }
}
