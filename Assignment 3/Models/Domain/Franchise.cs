using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment_3.models.Domain
{
    public class Franchise
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        //Deciding not to limit the length of description since description can be long.
        public string Description { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
