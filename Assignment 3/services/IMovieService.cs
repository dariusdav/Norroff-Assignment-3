using Assignment_3.models.Domain;
using Assignment_3.models.DTO.Character;
using Assignment_3.Models.DTO.Movie;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment_3.services
{
    public interface IMovieService
    {
        public  Task<List<MovieDTO>> GetMovies();
        public  Task<MovieDTO> GetMovie(int id);
        public Task<List<CharacterDTO>> GetCharacters(int id);
        public void PutMovie(MovieDTO movie);
        public List<Character> PutCharacters(int[] characterIds);
        public Movie PostMovie(MovieCreateDTO movie);
        public void DeleteMovie(Movie movie);
    }
}
