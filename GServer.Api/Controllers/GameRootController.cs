using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GServer.Api.Controllers
{
    [Route("api/gameroot")]
    [ApiController]
    public class GameRootController: ControllerBase
    {
        private readonly IGameRootRepository gameRootRepository;

        public GameRootController(IGameRootRepository gameRootRepository)
        {
            this.gameRootRepository = gameRootRepository;
        }

        [HttpGet]
        public async Task<IReadOnlyList<GameRoot>> Get() => await gameRootRepository.ListAllAsync();

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var game_root = await gameRootRepository.GetByIdAsync(id);
            if(game_root == null) {
                return NotFound();
            }
            return new OkObjectResult(game_root);
        }
    }
}