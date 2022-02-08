using Assignment_3.Controllers;
using Assignment_3.models.Domain;
using Assignment_3.models.DTO.Character;
using Assignment_3.models.DTO.Franchise;
using Assignment_3.Models.DTO.Franchise;
using Assignment_3.Models.DTO.Movie;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment_3.services
{
    public interface IFranchiseService
    {
        public Task<IEnumerable<Franchise>> GetFranchises();
        public  Task<FranchiseMovieDTO> GetFranchise(int id);
        public Task<List<MovieCreateDTO>> GetFranchiseMovies(int id);
        public  Task<List<CharacterDTO>> GetFranchiseCharacters(int id);
        public void PutMoviesFranchise(Franchise franchise, int[] moviesIds);
        public void PutFranchise(FranchiseDTO franchise);
        public Franchise PostFranchise(FranchiseCreateDTO franchise);
        public void DeleteFranchise(int id, Franchise franchise);
    }
}
