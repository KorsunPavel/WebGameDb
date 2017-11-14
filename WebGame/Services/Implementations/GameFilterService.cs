using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.Web.Models;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.Web.Services.Implementations
{
    public class GameFilterService : Interfaces.IGameFilterService
    {
        private IUnitOfWork _db;

        public GameFilterService(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        public IEnumerable<GameDTO> GetGamesByGenre(GenreDTO genre)
        {
            return _db.GetRepository<Genre>().Get(genre.Id).Games.Select(game => new GameDTO
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Key = game.Key
            });
        }

        public IEnumerable<GameDTO> GetGamesByPlatform(PlatformTypeDTO platform)
        {
            return _db.GetRepository<PlatformType>().Get(platform.Id).Games.Select(game => new GameDTO
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Key = game.Key
            });
        }
    }
}