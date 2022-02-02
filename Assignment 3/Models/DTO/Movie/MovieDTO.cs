namespace Assignment_3.Models.DTO.Movie
{
using Assignment_3.models.Domain;
using System.Collections.Generic;

    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Picture { get; set; }
        public string Trailer { get; set; }

        public List<Character> Characters { get; set; }

        public int FranchiseId { get; set; }
    }

}

