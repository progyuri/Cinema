using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    director = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    genre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sessions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    movie_id = table.Column<int>(type: "integer", nullable: false),
                    start_time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    hall_number = table.Column<int>(type: "integer", nullable: true),
                    price = table.Column<decimal>(type: "numeric(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessions", x => x.id);
                    table.ForeignKey(
                        name: "FK_sessions_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "movies",
                columns: new[] { "id", "created_date", "description", "director", "genre", "title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 10, 23, 16, 28, 427, DateTimeKind.Utc).AddTicks(4603), "Продолжение эпической саги о пустынной планете Арракис.", "Дени Вильнёв", "Фантастика", "Дюна: Часть вторая" },
                    { 2, new DateTime(2025, 7, 10, 23, 16, 28, 427, DateTimeKind.Utc).AddTicks(4605), "Продолжение приключений По и его друзей.", "Майк Митчелл", "Мультфильм", "Кунг-фу Панда 4" },
                    { 3, new DateTime(2025, 7, 10, 23, 16, 28, 427, DateTimeKind.Utc).AddTicks(4607), "Приквел к культовой франшизе о постапокалиптическом мире.", "Джордж Миллер", "Боевик", "Безумный Макс: Фуриоса" },
                    { 4, new DateTime(2025, 7, 10, 23, 16, 28, 427, DateTimeKind.Utc).AddTicks(4609), "Эпическая история о космическом путешествии для спасения человечества.", "Кристофер Нолан", "Научная фантастика", "Интерстеллар" },
                    { 5, new DateTime(2025, 7, 10, 23, 16, 28, 427, DateTimeKind.Utc).AddTicks(4610), "История любви на фоне трагического крушения легендарного лайнера.", "Джеймс Кэмерон", "Драма", "Титаник" }
                });

            migrationBuilder.InsertData(
                table: "sessions",
                columns: new[] { "id", "hall_number", "movie_id", "price", "start_time" },
                values: new object[,]
                {
                    { 1, 1, 1, 500m, new TimeSpan(0, 12, 0, 0, 0) },
                    { 2, 1, 1, 500m, new TimeSpan(0, 15, 30, 0, 0) },
                    { 3, 1, 1, 600m, new TimeSpan(0, 19, 0, 0, 0) },
                    { 4, 2, 2, 400m, new TimeSpan(0, 10, 0, 0, 0) },
                    { 5, 2, 2, 400m, new TimeSpan(0, 13, 0, 0, 0) },
                    { 6, 2, 2, 450m, new TimeSpan(0, 17, 0, 0, 0) },
                    { 7, 3, 3, 550m, new TimeSpan(0, 14, 0, 0, 0) },
                    { 8, 3, 3, 550m, new TimeSpan(0, 18, 30, 0, 0) },
                    { 9, 3, 3, 600m, new TimeSpan(0, 21, 0, 0, 0) },
                    { 10, 1, 4, 500m, new TimeSpan(0, 11, 0, 0, 0) },
                    { 11, 1, 4, 500m, new TimeSpan(0, 16, 0, 0, 0) },
                    { 12, 1, 4, 600m, new TimeSpan(0, 20, 30, 0, 0) },
                    { 13, 2, 5, 450m, new TimeSpan(0, 13, 30, 0, 0) },
                    { 14, 2, 5, 450m, new TimeSpan(0, 17, 30, 0, 0) },
                    { 15, 2, 5, 500m, new TimeSpan(0, 21, 30, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_sessions_movie_id",
                table: "sessions",
                column: "movie_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sessions");

            migrationBuilder.DropTable(
                name: "movies");
        }
    }
}
