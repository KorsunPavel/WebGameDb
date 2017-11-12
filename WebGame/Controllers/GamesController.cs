using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebGame.Data;
using WebGame.Data.DAL;

namespace WebGame.Controllers
{
    public class GamesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public GamesController()
        {
            _unitOfWork = new UnitOfWork();
        }

        // GET api/games
        //// User can get all games(GET URL: /games).
        public IHttpActionResult GetGames()
        {
            var games = _unitOfWork.GameRepository
                .Get(includeProperties: "Comments, Genres, PlatformTypes")
                .ProjectTo<GameDto>();
            return Ok(games);
        }

        [Route("game/{key}")]
        // GET api/game/{key}
        // User can get game details by key(GET URL: /game/{ key}).
        public IHttpActionResult GetGame(string key)
        {
            var game = _unitOfWork.GameRepository.GetByPropertyValue(g => g.Key == key).FirstOrDefault();
            if (game != null)
                return Ok(Mapper.Map<Game, GameDto>(game));
            else
                return NotFound();
        }

        // POST games/remove
        //User can delete game (POST URL: /games/remove).
        [Route("games/remove")]
        [HttpPost]
        public IHttpActionResult RemoveGames([FromBody] GameDto gameDto)
        {
            var game = _unitOfWork.GameRepository.GetByPropertyValue(g => g.Key == gameDto.Key).FirstOrDefault();
            if (game == null)
                return NotFound();
            _unitOfWork.GameRepository.Delete(game);
            _unitOfWork.Commit();
            return Ok();
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
            var game = Mapper.Map<Game>(gameDto);

            //
            foreach (var genreInDto in gameDto.Genres)
            {
                var genreResult = _unitOfWork.GenreRepository
                                        .GetByPropertyValue(g => g.Name == genreInDto)
                                                .FirstOrDefault();
                if (genreResult != null)
                {
                    game.Genres.Add(genreResult);
                }
            }
            foreach (var platformTypeInDto in gameDto.PlatformTypes)
            {
                var platformTypeResult = _unitOfWork.PlatformTypeRepository
                                        .GetByPropertyValue(g => g.Type == platformTypeInDto)
                                                .FirstOrDefault();
                if (platformTypeResult != null)
                {
                    game.PlatformTypes.Add(platformTypeResult);
                }
            }

            _unitOfWork.GameRepository.Insert(game);
            _unitOfWork.Commit();
            gameDto = Mapper.Map<GameDto>(gameDto);
            return CreatedAtRoute("DefaultApi", new { id = gameDto.Id }, gameDto);
        }

        // PUT api/games/new
        // User can edit game (POST URL: /games/update).
        [HttpPost]
        [Route("games/update")]
        [ResponseType(typeof(Game))]
        public IHttpActionResult UpdateGames([FromBody] GameDto gameDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var gameInDb = _unitOfWork.GameRepository.GetByPropertyValue(g => g.Key == gameDto.Key).FirstOrDefault();
            if (gameInDb == null)
                return NotFound();

            gameInDb.Genres.Clear();
            gameInDb.PlatformTypes.Clear();

            foreach (var genreInDto in gameDto.Genres)
            {
                var genreResult = _unitOfWork.GenreRepository
                                        .GetByPropertyValue(g => g.Name == genreInDto)
                                                .FirstOrDefault();
                if (genreResult != null)
                {
                    gameInDb.Genres.Add(genreResult);
                }
            }
            foreach (var platformTypeInDto in gameDto.PlatformTypes)
            {
                var platformTypeResult = _unitOfWork.PlatformTypeRepository
                                        .GetByPropertyValue(g => g.Type == platformTypeInDto)
                                                .FirstOrDefault();
                if (platformTypeResult != null)
                {
                    gameInDb.PlatformTypes.Add(platformTypeResult);
                }
            }

            _unitOfWork.GameRepository.Update(gameInDb);
            _unitOfWork.Commit();
            gameDto = Mapper.Map<GameDto>(gameDto);
            return Ok(gameDto);
        }
    }
}
