using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using PuckPro.Models;

namespace PuckPro.Controllers;


public class PlayerProperties
{
    public int number { get; set; }
    public float footSize { get; set; }
    public MainHandType mainHand { get; set; }
    public float height { get; set; }
    public float weight { get; set; }
    public PositionType position { get; set; }
}

public class CreatePlayerRequest
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string middleName { get; set; } = "-";
    public DateOnly dateOfBirth { get; set; }
    public int countryId { get; set; }
    public PlayerProperties playerPropeties { get; set; }
}


[ApiController]
public class PlayerController : ControllerBase
{
    private PuckDBContext _db;

    public PlayerController(PuckDBContext context)
    {
        _db = context;
    }
    
    [HttpPost("api/players/", Name = "Добавить игрока")]
    public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerRequest _body)
    {
        var player = new Players
        {
            FirstName = _body.firstName,
            LastName = _body.lastName,
            MiddleName = _body.middleName,
            DateBirth = _body.dateOfBirth,
            CountryId = _body.countryId
        };

        _db.Players.Add(player);

        var properties = new PlayersProperties
        {
            Player = player,
            Number = _body.playerPropeties.number,
            FootSize = _body.playerPropeties.footSize,
            MainHand = _body.playerPropeties.mainHand,
            Height = _body.playerPropeties.height,
            Weight = _body.playerPropeties.weight,
            Position = _body.playerPropeties.position
        };

        _db.PlayersProperties.Add(properties);

        await _db.SaveChangesAsync();

        return Ok(new { playerId = player.Id });
    }
}