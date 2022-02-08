using Assignment_3.models;
using Assignment_3.models.Domain;
using Assignment_3.models.DTO.Character;
using Assignment_3.Models.DTO.Movie;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_3.services
{
    public class MovieService : IMovieService
    {
        private readonly MovieCharactersDbContext _context;
        private readonly IMapper _mapper;

        public MovieService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public MovieService(MovieCharactersDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all the movies.
        /// </summary>
        /// <returns></returns>
        public async Task<List<MovieDTO>> GetMovies()
        {
            return _mapper.Map<List<MovieDTO>>(await _context.Movies.ToListAsync());
        }
        /// <summary>
        /// Returns a specific movie.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MovieDTO> GetMovie(int id)
        {
            return  _mapper.Map<MovieDTO>(await _context.Movies
                .Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id));
        }
        /// <summary>
        /// Returns all the characters in a specific movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<CharacterDTO>> GetCharacters(int id)
        {
            return _mapper.Map<List<CharacterDTO>>(await _context.Movies
                .Where(m => m.Id == id)
                .SelectMany(m => m.Characters)
                .Select(c => c).ToListAsync());
        }
       /// <summary>
       /// Update an existing movie.
       /// </summary>
       /// <param name="movie"></param>
        public void PutMovie(MovieDTO movie)
        {
            _context.Entry(_mapper.Map<Movie>(movie)).State = EntityState.Modified;
        }
        public  List<Character> PutCharacters(int[] characterIds)
        {
            List<Character> cList = new();
            for (int i = 0; i < characterIds.Length; i++)
            {
                cList.Add(_context.Characters.Where(c => c.Id == characterIds[i]).First());
            }
           return cList;
        }
        /// <summary>
        /// Updates existing movie.
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public  Movie PostMovie(MovieCreateDTO movie)
        {
            return _context.Movies.Add(_mapper.Map<Movie>(movie)).Entity;
        }

        public void DeleteMovie(Movie movie) {
            _context.Movies.Remove(movie);
        }
    }
}
