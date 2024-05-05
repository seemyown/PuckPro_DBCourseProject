using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PuckPro.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    city_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    country_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_country_id",
                        column: x => x.country_id,
                        principalTable: "Countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    date_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    homeland_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.id);
                    table.ForeignKey(
                        name: "FK_Players_Countries_homeland_id",
                        column: x => x.homeland_id,
                        principalTable: "Countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    city_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.id);
                    table.ForeignKey(
                        name: "FK_Teams_Cities_city_id",
                        column: x => x.city_id,
                        principalTable: "Cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerSalaries",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    player_id = table.Column<long>(type: "bigint", nullable: false),
                    salary = table.Column<float>(type: "real", nullable: false),
                    bonus = table.Column<float>(type: "real", nullable: false),
                    fine = table.Column<float>(type: "real", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSalaries", x => x.id);
                    table.ForeignKey(
                        name: "FK_PlayerSalaries_Players_player_id",
                        column: x => x.player_id,
                        principalTable: "Players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayersProperties",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    player_id = table.Column<long>(type: "bigint", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    foot_size = table.Column<float>(type: "real", nullable: false),
                    main_hand = table.Column<int>(type: "integer", nullable: false),
                    height = table.Column<float>(type: "real", nullable: false),
                    weight = table.Column<float>(type: "real", nullable: false),
                    position = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersProperties", x => x.id);
                    table.ForeignKey(
                        name: "FK_PlayersProperties_Players_player_id",
                        column: x => x.player_id,
                        principalTable: "Players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayersRatings",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    player_id = table.Column<long>(type: "bigint", nullable: false),
                    total_games = table.Column<int>(type: "integer", nullable: false),
                    total_goals = table.Column<int>(type: "integer", nullable: false),
                    total_assists = table.Column<int>(type: "integer", nullable: false),
                    total_fine_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    rating = table.Column<float>(type: "real", nullable: false),
                    actual_on_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersRatings", x => x.id);
                    table.ForeignKey(
                        name: "FK_PlayersRatings_Players_player_id",
                        column: x => x.player_id,
                        principalTable: "Players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    home_team_id = table.Column<long>(type: "bigint", nullable: false),
                    guest_team_id = table.Column<long>(type: "bigint", nullable: false),
                    city_id = table.Column<int>(type: "integer", nullable: false),
                    game_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.id);
                    table.ForeignKey(
                        name: "FK_Games_Cities_city_id",
                        column: x => x.city_id,
                        principalTable: "Cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Teams_guest_team_id",
                        column: x => x.guest_team_id,
                        principalTable: "Teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Teams_home_team_id",
                        column: x => x.home_team_id,
                        principalTable: "Teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamRoasters",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    player_id = table.Column<long>(type: "bigint", nullable: false),
                    team_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamRoasters", x => x.id);
                    table.ForeignKey(
                        name: "FK_TeamRoasters_Players_player_id",
                        column: x => x.player_id,
                        principalTable: "Players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamRoasters_Teams_team_id",
                        column: x => x.team_id,
                        principalTable: "Teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamStatistics",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    team_id = table.Column<long>(type: "bigint", nullable: false),
                    wins = table.Column<int>(type: "integer", nullable: false),
                    lose = table.Column<int>(type: "integer", nullable: false),
                    popularity = table.Column<float>(type: "real", nullable: false),
                    actual_on_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStatistics", x => x.id);
                    table.ForeignKey(
                        name: "FK_TeamStatistics_Teams_team_id",
                        column: x => x.team_id,
                        principalTable: "Teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameRevenues",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    game_id = table.Column<long>(type: "bigint", nullable: false),
                    sold_tickets = table.Column<int>(type: "integer", nullable: false),
                    total_viewers = table.Column<int>(type: "integer", nullable: false),
                    Income = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRevenues", x => x.id);
                    table.ForeignKey(
                        name: "FK_GameRevenues_Games_game_id",
                        column: x => x.game_id,
                        principalTable: "Games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamesResults",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    game_id = table.Column<long>(type: "bigint", nullable: false),
                    winner_team_id = table.Column<long>(type: "bigint", nullable: false),
                    winner_count = table.Column<int>(type: "integer", nullable: false),
                    loser_count = table.Column<int>(type: "integer", nullable: false),
                    total_played_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    has_overtime = table.Column<bool>(type: "boolean", nullable: false),
                    has_penalty = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesResults", x => x.id);
                    table.ForeignKey(
                        name: "FK_GamesResults_Games_game_id",
                        column: x => x.game_id,
                        principalTable: "Games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamesResults_Teams_winner_team_id",
                        column: x => x.winner_team_id,
                        principalTable: "Teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStatisticPerGames",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    player_id = table.Column<long>(type: "bigint", nullable: false),
                    game_id = table.Column<long>(type: "bigint", nullable: false),
                    goals = table.Column<int>(type: "integer", nullable: false),
                    assists = table.Column<int>(type: "integer", nullable: false),
                    fine_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatisticPerGames", x => x.id);
                    table.ForeignKey(
                        name: "FK_PlayerStatisticPerGames_Games_game_id",
                        column: x => x.game_id,
                        principalTable: "Games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerStatisticPerGames_Players_player_id",
                        column: x => x.player_id,
                        principalTable: "Players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_country_id",
                table: "Cities",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_GameRevenues_game_id",
                table: "GameRevenues",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "IX_Games_city_id",
                table: "Games",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_Games_guest_team_id",
                table: "Games",
                column: "guest_team_id");

            migrationBuilder.CreateIndex(
                name: "IX_Games_home_team_id",
                table: "Games",
                column: "home_team_id");

            migrationBuilder.CreateIndex(
                name: "IX_GamesResults_game_id",
                table: "GamesResults",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "IX_GamesResults_winner_team_id",
                table: "GamesResults",
                column: "winner_team_id");

            migrationBuilder.CreateIndex(
                name: "IX_Players_homeland_id",
                table: "Players",
                column: "homeland_id");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSalaries_player_id",
                table: "PlayerSalaries",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "IX_PlayersProperties_player_id",
                table: "PlayersProperties",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "IX_PlayersRatings_player_id",
                table: "PlayersRatings",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatisticPerGames_game_id",
                table: "PlayerStatisticPerGames",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatisticPerGames_player_id",
                table: "PlayerStatisticPerGames",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "IX_TeamRoasters_player_id",
                table: "TeamRoasters",
                column: "player_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamRoasters_team_id",
                table: "TeamRoasters",
                column: "team_id");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_city_id",
                table: "Teams",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "teams_names_uniq_index",
                table: "Teams",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamStatistics_team_id",
                table: "TeamStatistics",
                column: "team_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameRevenues");

            migrationBuilder.DropTable(
                name: "GamesResults");

            migrationBuilder.DropTable(
                name: "PlayerSalaries");

            migrationBuilder.DropTable(
                name: "PlayersProperties");

            migrationBuilder.DropTable(
                name: "PlayersRatings");

            migrationBuilder.DropTable(
                name: "PlayerStatisticPerGames");

            migrationBuilder.DropTable(
                name: "TeamRoasters");

            migrationBuilder.DropTable(
                name: "TeamStatistics");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
