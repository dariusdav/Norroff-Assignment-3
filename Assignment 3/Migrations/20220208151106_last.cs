using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment_3.Migrations
{
    public partial class last : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Franchises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReleaseYear = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    Director = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Trailer = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    FranchiseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Franchises_FranchiseId",
                        column: x => x.FranchiseId,
                        principalTable: "Franchises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CharacterMovie",
                columns: table => new
                {
                    CharactersId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMovie", x => new { x.CharactersId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Characters_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Alias", "Gender", "Name", "Picture" },
                values: new object[,]
                {
                    { 1, "Iron Man", "Male", "Tony Stark", "https://www.imdb.com/title/tt0371746/mediaviewer/rm286559232/?ref_=tt_md_4" },
                    { 2, "War Machine", "Male", "James Rhodey", "https://www.imdb.com/title/tt1228705/mediaviewer/rm3414265088/?ref_=tt_md_4" },
                    { 3, "", "Female", "Pepper Potts", "https://m.media-amazon.com/images/M/MV5BOGI2NTMxNTEtMjcxMS00NTk1LTgwZDYtZGNjOGNlMjRiNmVlXkEyXkFqcGdeQXVyMzkyOTg1MzE@._V1_SY200_CR79,0,200,200_AL_.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Franchises",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, " The Marvel Cinematic Universe (MCU) is an American media franchise and shared universe centered on a series of superhero films produced by Marvel Studios. The films are based on characters that appear in American comic books published by Marvel Comics. The franchise also includes television series, short films, digital series, and literature. The shared universe, much like the original Marvel Universe in comic books, was established by crossing over common plot elements, settings, cast, and characters.", "Marvel Cinematic Universe" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "Picture", "ReleaseYear", "Title", "Trailer" },
                values: new object[] { 1, "Jon Favreau", 1, " Action, Adventure, Sci fi", "https://www.imdb.com/title/tt0371746/mediaviewer/rm1544850432/?ref_=tt_ov_i", "2008", "Iron Man", "https://www.imdb.com/video/vi447873305?playlistId=tt0371746&ref_=tt_pr_ov_vi" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "Picture", "ReleaseYear", "Title", "Trailer" },
                values: new object[] { 2, "Jon Favreau", 1, " Action, Adventure, Sci fi", "https://www.imdb.com/title/tt1228705/mediaviewer/rm1059163136/?ref_=tt_ov_i", "2010", "Iron Man 2", "https://www.imdb.com/video/vi2256077849?playlistId=tt1228705&ref_=tt_pr_ov_vi" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "Picture", "ReleaseYear", "Title", "Trailer" },
                values: new object[] { 3, "Shane Black", 1, " Action, Adventure, Sci fi", "https://www.imdb.com/title/tt1300854/mediaviewer/rm520987392/?ref_=tt_ov_i", "2013", "Iron Man 3", "https://www.imdb.com/video/vi2830738969?playlistId=tt1300854&ref_=tt_pr_ov_vi" });

            migrationBuilder.InsertData(
                table: "CharacterMovie",
                columns: new[] { "CharactersId", "MoviesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMovie_MoviesId",
                table: "CharacterMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_FranchiseId",
                table: "Movies",
                column: "FranchiseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterMovie");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Franchises");
        }
    }
}
