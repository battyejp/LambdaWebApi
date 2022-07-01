using Amazon.DynamoDBv2.DataModel;
using LambdaWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LambdaWebApi.Controllers;

[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly IDynamoDBContext context;
    private readonly ILogger<TeamsController> _logger;

    public TeamsController(IDynamoDBContext context, ILogger<TeamsController> logger)
    {
        this.context = context;
        _logger = logger;
    }


    // GET api/teams/football
    [HttpGet("{sportType}")]
    public async Task<IActionResult> Get(string sportType)
    {
        try
        {
            var result = context.QueryAsync<SportsTeam>(sportType);

            if (result == null)
                return Ok();

            return Ok(await result.GetRemainingAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    // POST api/teams
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]SportsTeam sportsTeam)
    {
        try
        {
            await context.SaveAsync(sportsTeam);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, ex.Message);
        }
    }
}