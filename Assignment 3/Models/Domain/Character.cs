using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment_3.models.Domain
{
    public class Character

    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]

        public string Alias { get; set; }
        [MaxLength(10)]

        public string Gender { get; set; }
        [Url]
        public string Picture { get; set; }
        public ICollection<Movie> Movies { get; set; }

    }
}