using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameStore.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using WebGame.Data;
using WebGame.Data.DAL;

namespace WebGame.Controllers
{
    public class GamesController : ApiController
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService )
        {
            _gameService = gameService;
        }

        // GET api/games
        //// User can get all games(GET URL: /games).
        public IHttpActionResult GetGames()
        {
            var gamesDto = _gameService.GetAllGames();
            return Ok(gamesDto);
        }


        [Route("game/{key}")]
        // GET api/game/{key}
        // User can get game details by key(GET URL: /game/{ key}).
        public IHttpActionResult GetGame(string key)
        {
            var gameDto =  _gameService.GetGameByKey(key);
            if (gameDto != null)
                return Ok(gameDto);
            else
                return NotFound();
        }

        // POST games/remove
        //User can delete game (POST URL: /games/remove).
        [Route("games/remove")]
        [HttpDelete]
        public IHttpActionResult RemoveGames(string key)
        {
            bool result = _gameService.DeleteGame(key);
            if (result)
                return Ok();
            return NotFound();
        }

        // POST api/games/new
        // User can create game (POST URL: /games/new).
        [HttpPost]
        [ResponseType(typeof(Game))]
        public IHttpActionResult NewGames(GameDto gameDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            gameDto = _gameService.CreateNewGame(gameDto);
            return Content(HttpStatusCode.Created , gameDto); 
        }

        // PUT api/games/new
        // User can edit game (POST URL: /games/update).
        [HttpPut]
        [Route("games/update")]
        [ResponseType(typeof(Game))]
        public IHttpActionResult UpdateGames([FromBody] GameDto gameDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_gameService.EditGame(gameDto))
                return Created("DefaultApi", gameDto);
            return BadRequest(ModelState);
        }
    }
}
