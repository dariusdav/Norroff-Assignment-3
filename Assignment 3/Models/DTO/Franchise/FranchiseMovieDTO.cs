using Assignment_3.models.Domain;
using Assignment_3.Models.DTO.Movie;
using System.Collections.Generic;

namespace Assignment_3.models.DTO.Franchise
{
    /// <summary>
    /// Data Transfer
    /// </summary>
    public class FranchiseMovieDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }
        //Deciding not to limit the length of description since description can be long.
        public List<int> Movies { get; set; }
    }
}
