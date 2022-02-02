using Assignment_3.models.Domain;
using System.Collections.Generic;

namespace Assignment_3.models.DTO.Franchise
{
    public class FranchiseDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }
        //Deciding not to limit the length of description since description can be long.
        public List<Movie> Movies { get; set; }
    }
}
