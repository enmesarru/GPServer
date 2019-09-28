using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using FluentValidation.AspNetCore;
using GServer.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GServer.Api.Controllers
{
    [Authorize]
    [Route("api/game")]
    [ApiController]
    public class GameController: ControllerBase
    {
        private readonly IGameRepository gameRepository;
        private readonly IMapper mapper;

        public GameController(IGameRepository gameRepository, IMapper mapper)
        {
            this.gameRepository = gameRepository;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IReadOnlyList<GameViewModel>> Get()
        {
            var games = await gameRepository.ListAllGames();
            var game_mapper = mapper.Map<IReadOnlyList<GameViewModel>>(games);
            return game_mapper;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var game = await gameRepository.GetGameWithId(id);
            if(game == null)
            {
                return NotFound();
            }
            return new OkObjectResult(game);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GameViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var game_model = mapper.Map<Game>(model);
            await gameRepository.AddAsync(game_model);

            return new OkObjectResult(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            Guid id, 
            [CustomizeValidator(Properties="CategoryId,GameRootId,Name,Link,Description,ImageURL")]
            [FromBody] GameViewModel model
        )
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var game = await gameRepository.GetByIdAsync(id);
            if(game is null)
            {
                return NotFound();
            }

            var _mapper = mapper.Map(model, game);
            await gameRepository.UpdateAsync(_mapper);
            return Ok(_mapper);
        }
    }
}