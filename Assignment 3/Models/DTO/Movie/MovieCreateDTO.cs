﻿namespace Assignment_3.Models.DTO.Movie
{
    public class MovieCreateDTO
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Picture { get; set; }
        public string Trailer { get; set; }
        public int FranchiseId { get; set; }
    }
}
