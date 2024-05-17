using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PuckPro.Models
{
    public class Countries
    {
        [Column("id")][Key] public int Id { get; set; }
        [Column("name")] public string? Name { get; set; }

        public virtual ICollection<Cities> Cities { get; set; } = new List<Cities>();
        public virtual ICollection<Players> Players { get; set; } = new List<Players>();
    }

    public class Cities
    {
        [Column("id")][Key] public int Id { get; set; }
        [Column("name")] public string? Name { get; set; }
        [Column("country_id")][ForeignKey(nameof(Country))] public int CountryId { get; set; }

        public virtual Countries? Country { get; set; }
        public virtual ICollection<Teams> Teams { get; set; } = new List<Teams>();
        public virtual ICollection<Games> Games { get; set; } = new List<Games>();
    }

    public class Players
    {
        [Column("id")][Key] public long Id { get; set; }
        [Column("first_name")][Required] public string? FirstName { get; set; }
        [Column("last_name")][Required] public string? LastName { get; set; }
        [Column("middle_name")] public string? MiddleName { get; set; }
        [Column("date_birth")][Required] public DateOnly DateBirth { get; set; }
        [Column("homeland_id")][ForeignKey(nameof(Country))] public int CountryId { get; set; }
        
        public virtual Countries? Country { get; set; }
        public virtual ICollection<PlayersProperties> Properties { get; set; } = new List<PlayersProperties>();
        public virtual ICollection<PlayerSalary> Salaries { get; set; } = new List<PlayerSalary>();
        public virtual ICollection<PlayerStatisticPerGame> StatisticPerGame { get; set; } =
            new List<PlayerStatisticPerGame>();
        
        public virtual TeamRoasters? Team { get; set; }
    }

    public enum MainHandType
    {
        [Description("right")]
        Right,
        [Description("left")]
        Left
    }

    public enum PositionType
    {
        [Description("forward")]
        Forward,
        [Description("defender")]
        Defender,
        [Description("goalkeeper")]
        GoalKeeper
    }

    public class PlayersProperties
    {
        [Column("id")][Key] public long Id { get; set; }
        [Column("player_id")][ForeignKey(nameof(Player))] public long PlayerId { get; set; }
        [Column("number")][Required] public int Number { get; set; }
        [Column("foot_size")][Required] public float FootSize { get; set; }
        [Column("main_hand")][Required] public MainHandType MainHand { get; set; }
        [Column("height")][Required] public float Height { get; set; }
        [Column("weight")][Required] public float Weight { get; set; }
        [Column("position")][Required] public PositionType Position { get; set; }
        
        public virtual Players? Player { get; set; }
    }

    public class PlayerSalary
    {
        [Column("id")][Key] public long Id { get; set; }
        [Column("player_id")][ForeignKey(nameof(Player))] public long PlayerId { get; set; }
        [Column("salary")] public float Money { get; set; }
        [Column("bonus")] public float Bonus { get; set; }
        [Column("fine")] public float Fine { get; set; }
        [Column("date_create")] public DateTime DateCreate { get; set; }
        
        public virtual Players? Player { get; set; }
    }

    public class PlayersRating
    {
        [Column("id")][Key] public long Id { get; set; }
        [Column("player_id")][ForeignKey(nameof(Player))] public long PlayerId { get; set; }
        [Column("total_games")] public int TotalGames { get; set; }
        [Column("total_goals")] public int TotalGoals { get; set; }
        [Column("total_assists")] public int TotalAssists { get; set; }
        [Column("total_fine_time")] public TimeOnly TotalFineTime { get; set; }
        [Column("rating")] public float Rating { get; set; }
        [Column("actual_on_date")] public DateTime ActualOnDate { get; set; }
        
        public virtual Players? Player { get; set; }
    }

    [Index("Name", IsUnique = true, Name = "teams_names_uniq_index")]
    public class Teams
    {
        [Column("id")][Key] public long Id { get; set; }
        [Column("name")] public string? Name { get; set; }
        [Column("city_id")][ForeignKey(nameof(City))] public int CityId { get; set; }
        
        public virtual Cities? City { get; set; }
        public virtual ICollection<TeamRoasters> Roaster { get; set; } = new List<TeamRoasters>();
        public virtual ICollection<TeamStatistic> Statistics { get; set; } = new List<TeamStatistic>();

        public virtual ICollection<Games> HomeGames { get; set; } = new List<Games>();
        public virtual ICollection<Games> AwayGames { get; set; } = new List<Games>();

        public virtual ICollection<GamesResults> WonGames { get; set; } = new List<GamesResults>();
    }

    public class TeamRoasters
    {
        [Column("id")][Key] public long Id { get; set; }
        [Column("player_id")][ForeignKey(nameof(Player))] public long PlayerId { get; set; }
        [Column("team_id")][ForeignKey(nameof(Team))] public long TeamId { get; set; }
        
        public virtual Teams? Team { get; set; }
        public virtual Players? Player { get; set; }
    }

    public class TeamStatistic
    {
        [Column("id")][Key] public long Id { get; set; }
        [Column("team_id")][ForeignKey(nameof(Team))] public long TeamId { get; set; }
        [Column("wins")] public int Wins { get; set; }
        [Column("lose")] public int Lose { get; set; }
        [Column("popularity")] public float Popularity { get; set; }
        [Column("actual_on_date")] public DateTime ActualOnDate { get; set; }
        
        public virtual Teams? Team { get; set; }
    }

    public class Games
    {
        [Column("id")][Key] public long Id { get; set; }
        [Column("home_team_id")][ForeignKey(nameof(HomeTeam))] public long HomeTeamId { get; set; }
        [Column("guest_team_id")][ForeignKey(nameof(GuestTeam))] public long GuestTeamId { get; set; }
        [Column("city_id")][ForeignKey(nameof(City))] public int CityId { get; set; }
        [Column("game_date")] public DateTime GameDate { get; set; }
        
        public virtual Teams? HomeTeam { get; set; }
        public virtual Teams? GuestTeam { get; set; }
        public virtual Cities? City { get; set; }

        public virtual ICollection<GamesResults> Results { get; set; } = new List<GamesResults>();
        public virtual ICollection<PlayerStatisticPerGame> PlayerStatistic { get; set; } =
            new List<PlayerStatisticPerGame>();
        public virtual ICollection<GameRevenue> Revenues { get; set; } = new List<GameRevenue>();
    }

    public class GameRevenue
    {
        [Column("id")][Key] public long Id { get; set; }
        [Column("game_id")][ForeignKey(nameof(Game))] public long GameId { get; set; }
        [Column("sold_tickets")] public int SoldTickets { get; set; }
        [Column("total_viewers")] public int TotalViewers { get; set; }
        [Column("Income")] public float Income { get; set; }
        
        public virtual Games? Game { get; set; }
    }

    public class GamesResults
    {
        [Column("id")][Key] public long Id { get; set; }
        [Column("game_id")][ForeignKey(nameof(Game))] public long GameId { get; set; }
        [Column("winner_team_id")][ForeignKey(nameof(Team))] public long WinnerTeamId { get; set; }
        [Column("winner_count")] public int WinnerCount { get; set; }
        [Column("loser_count")] public int LoserCount { get; set; }
        [Column("total_played_time")] public TimeOnly TotalPlayedTime { get; set; }
        [Column("has_overtime")] public bool HasOverTime { get; set; }
        [Column("has_penalty")] public bool HasPenalty { get; set; }
        
        public virtual Games? Game { get; set; }
        public virtual Teams? Team { get; set; }
    }

    public class PlayerStatisticPerGame
    {
        [Column("id")][Key] public long Id { get; set; }
        [Column("player_id")][ForeignKey(nameof(Player))] public long PlayerId { get; set; }
        [Column("game_id")][ForeignKey(nameof(Game))] public long GameId { get; set; }
        [Column("goals")] public int Goals { get; set; }
        [Column("assists")] public int Assists { get; set; }
        [Column("fine_time")] public TimeOnly FineTime { get; set; }
        
        public virtual Players? Player { get; set; }
        public virtual Games? Game { get; set; }
    }
}