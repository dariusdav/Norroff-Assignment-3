using Assignment_3.Controllers;
using Assignment_3.models;
using Assignment_3.models.Domain;
using Assignment_3.models.DTO.Character;
using Assignment_3.models.DTO.Franchise;
using Assignment_3.Models.DTO.Franchise;
using Assignment_3.Models.DTO.Movie;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_3.services
{
    public class FranchiseService : IFranchiseService
    {
        private readonly MovieCharactersDbContext _context;
        private readonly IMapper _mapper;

        public FranchiseService(MovieCharactersDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets all franchises async.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Franchise>> GetFranchises()
        {
            return await _context.Franchises.ToListAsync();
        }
        /// <summary>
        /// Gets a specific franchise async.
        /// </summary>
        /// <returns></returns>
        public async Task<FranchiseMovieDTO> GetFranchise(int id)
        {
            return _mapper.Map<FranchiseMovieDTO>(await _context.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(f => f.Id == id));
        }

        /// <summary>
        /// Gets all movies in a franchise async.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<MovieCreateDTO>> GetFranchiseMovies(int id)
        {
            List<MovieCreateDTO> movies = _mapper.Map<List<MovieCreateDTO>>(await _context.Movies
                .Where(m => m.FranchiseId == id).ToListAsync());
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise == null)
            {
                return null;
            }
            return movies;
        }
        /// <summary>
        /// Gets all characters in a franchise async.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<CharacterDTO>> GetFranchiseCharacters(int id)
        {
            // First getting all characterID's of all the movies in the franchise
            List<int> characterID = await _context.Movies
                .Where(m => m.FranchiseId == id)
                .SelectMany(m => m.Characters)
                .Select(c => c.Id)
                .ToListAsync();
            // Converting all the characters to DTO's to be returned.
            List<CharacterDTO> characters = await _context.Characters
                .Where(c => characterID.Contains(c.Id))
                .Select(c => _mapper.Map<CharacterDTO>(c)).ToListAsync();
            var franchise = await _context.Franchises.FindAsync(id);
            //franchise.Movies = movies;
            if (franchise == null)
            {
                return null;
            }
            return characters;
        }
        /// <summary>
        /// Updates movies that belong to a franchise async.
        /// </summary>
        /// <param name="franchise"></param>
        /// <param name="moviesIds"></param>
        public void PutMoviesFranchise(Franchise franchise, int[] moviesIds)
        {
            List<Movie> mList = new();
            for (int i = 0; i < moviesIds.Length; i++)
            {
                mList.Add(_context.Movies.Where(c => c.Id == moviesIds[i]).First());
            }
            franchise.Movies = mList;
            _context.Entry(_mapper.Map<Franchise>(franchise)).State = EntityState.Modified;
        }
        /// <summary>
        /// updates a franchise.
        /// </summary>
        /// <param name="franchise"></param>
        public void PutFranchise (FranchiseDTO franchise)
        {
            _context.Entry(_mapper.Map<Franchise>(franchise)).State = EntityState.Modified;
        }

        /// <summary>
        /// creates a new franchise async.
        /// </summary>
        /// <param name="franchise"></param>
        /// <returns></returns>
        public Franchise PostFranchise(FranchiseCreateDTO franchise)
        {
            return _context.Franchises.Add(_mapper.Map<Franchise>(franchise)).Entity;
        }
        /// <summary>
        /// Deletes a franchise async.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="franchise"></param>
        public async void DeleteFranchise(int id, Franchise franchise)
        {
            List<Movie> m = await _context.Movies.Where(m => m.FranchiseId == id).ToListAsync();
            foreach (Movie movie in m)
            {
                movie.FranchiseId = null;
            }
            _context.Franchises.Remove(franchise);
        }

    }
}
