using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment_3.models.Domain
{
    public class Movie 
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(50)]
        public string Genre { get; set; }
        [MaxLength(8)]
        public string ReleaseYear { get; set; }
        [MaxLength(20)]
        public string Director { get; set; }
        [Url]
        [MaxLength(300)]
        public string Picture { get; set; }
        [Url]
        [MaxLength(300)]
        public string Trailer { get; set; }
        [MaxLength(50)]

        public ICollection<Character> Characters { get; set; }

        public Franchise Franchise { get; set; }
        public int? FranchiseId { get; set; }
    }
}
