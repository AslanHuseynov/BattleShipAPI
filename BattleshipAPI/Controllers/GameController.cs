using BattleshipAPI.Models.Game;
using Company.Persistence.DB;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public GameController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost("save-game-result")]
        public ActionResult SaveGameResult(BattleshipGame game)
        {
            try
            {
                var resultEntity = new GameResultEntity
                {
                    StartTime = game.StartTime,
                    Result = game.Result,
                    NumberOfMoves = game.NumberOfMoves
                };

                _dataContext.GameResultEntities.Add(resultEntity);
                _dataContext.SaveChanges();
                
                return Ok("Game result saved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("game-results")]
        public ActionResult<IEnumerable<GameResultEntity>> GetGameResults()
        {
            try
            {
                var gameResults = _dataContext.GameResultEntities.OrderBy(r => r.Result).ThenBy(r => r.NumberOfMoves).ToList();
                return Ok(gameResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
