using Assignment_3.models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assignment_3.models
{
    public class MovieCharactersDbContext : DbContext
    {
        public MovieCharactersDbContext(DbContextOptions options) : base(options) { }

        //Tables
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Franchise> Franchises { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source =DESKTOP-NJTO29J\\SQLEXPRESS; Initial Catalog= Movie Characters Db; Integrated Security = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Franchise>().HasData(
                new Franchise
                {
                    Id = 1,
                    Name = "Marvel Cinematic Universe",
                    Description = " The Marvel Cinematic Universe (MCU) is an " +
                    "American media franchise and shared universe centered on a " +
                    "series of superhero films produced by Marvel Studios. The films" +
                    " are based on characters that appear in American comic books" +
                    " published by Marvel Comics. The franchise also includes television" +
                    " series, short films, digital series, and literature. The shared " +
                    "universe, much like the original Marvel Universe in comic books," +
                    " was established by crossing over common plot elements, settings," +
                    " cast, and characters."
                });
            modelBuilder.Entity<Franchise>().HasData(
                new Franchise
                {
                    Id = 2,
                    Name = "Lord Of The Rings",
                    Description = " Rings and hobbits and MY AXE."
                });
            modelBuilder.Entity<Franchise>().HasData(
             new Franchise
             {
                 Id = 3,
                 Name = "DCEU",
                 Description = " because nr1 exists."
             });
            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1,Title = "Iron Man", 
                    Director= "Jon Favreau", 
                    Genre = " Action, Adventure, Sci fi", 
                    ReleaseYear = "2008",
                    Trailer = "https://www.imdb.com/video/vi447873305?playlistId=tt0371746&ref_=tt_pr_ov_vi",
                    Picture = "https://www.imdb.com/title/tt0371746/mediaviewer/rm1544850432/?ref_=tt_ov_i",
                    FranchiseId = 1
                });
            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 2,Title = "Iron Man 2", 
                    Director= "Jon Favreau", 
                    Genre = " Action, Adventure, Sci fi", 
                    ReleaseYear = "2010",
                    Trailer = "https://www.imdb.com/video/vi2256077849?playlistId=tt1228705&ref_=tt_pr_ov_vi",
                    Picture = "https://www.imdb.com/title/tt1228705/mediaviewer/rm1059163136/?ref_=tt_ov_i",
                    FranchiseId = 1
                });
            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 3,Title = "Iron Man 3", 
                    Director= "Shane Black", 
                    Genre = " Action, Adventure, Sci fi", 
                    ReleaseYear = "2013",
                    Trailer = "https://www.imdb.com/video/vi2830738969?playlistId=tt1300854&ref_=tt_pr_ov_vi",
                    Picture = "https://www.imdb.com/title/tt1300854/mediaviewer/rm520987392/?ref_=tt_ov_i",
                    FranchiseId = 1
                });
            modelBuilder.Entity<Character>().HasData(
                new Character
                {
                    Id = 1,
                    Name = "Tony Stark",
                    Alias = "Iron Man",
                    Gender = "Male",
                    Picture = "https://www.imdb.com/title/tt0371746/mediaviewer/rm286559232/?ref_=tt_md_4"
                });
            modelBuilder.Entity<Character>().HasData(
                new Character
                {
                    Id = 2,
                    Name = "James Rhodey",
                    Alias = "War Machine",
                    Gender = "Male",
                    Picture = "https://www.imdb.com/title/tt1228705/mediaviewer/rm3414265088/?ref_=tt_md_4"
                });
            modelBuilder.Entity<Character>().HasData(
                new Character
                {
                    Id = 3,
                    Name = "Pepper Potts",
                    Alias = "",
                    Gender = "Female",
                    Picture = "https://m.media-amazon.com/images/M/MV5BOGI2NTMxNTEtMjcxMS00NTk1LTgwZDYtZGNjOGNlMjRiNmVlXkEyXkFqcGdeQXVyMzkyOTg1MzE@._V1_SY200_CR79,0,200,200_AL_.jpg"
                });

                // Seed m2m coach-certification.Need to define m2m and access linking table
            modelBuilder.Entity<Character>()
                .HasMany(c => c.Movies)
                .WithMany(m => m.Characters)
                .UsingEntity<Dictionary<string, object>>(
                    "CharacterMovie",
                    r => r.HasOne<Movie>().WithMany().HasForeignKey("MoviesId"),
                    l => l.HasOne<Character>().WithMany().HasForeignKey("CharactersId"),
                    je =>
                    {
                        je.HasKey("CharactersId", "MoviesId");
                        je.HasData(
                            new { CharactersId = 1, MoviesId = 1 },
                            new { CharactersId = 2, MoviesId = 1 },
                            new { CharactersId = 3, MoviesId = 1 },
                            new { CharactersId = 1, MoviesId = 2 },
                            new { CharactersId = 2, MoviesId = 2 },
                            new { CharactersId = 3, MoviesId = 2 },
                            new { CharactersId = 1, MoviesId = 3 },
                            new { CharactersId = 2, MoviesId = 3 },
                            new { CharactersId = 3, MoviesId = 3 }

                        );
                    });

        }
    }
}
