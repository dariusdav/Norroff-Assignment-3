using Assignment_3.models.Domain;
using Assignment_3.models.DTO.Franchise;
using AutoMapper;

namespace Assignment_3.Profiles
{
    public class FranchiseProfile : Profile
    {
            public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseDTO>();
        }
    }
}
