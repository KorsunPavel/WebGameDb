using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameStore.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebGame.Data;
using WebGame.Data.DAL;

namespace GameStore.Web.Services.Implementations
{
    public class GameService : IGameService
    {
        private IUnitOfWork _unitOfWork;

        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public GameDto CreateNewGame(GameDto gameDto)
        {
            var game = Mapper.Map<Game>(gameDto);

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
            return gameDto;
        }

        public bool DeleteGame(string key)
        {
            var game = _unitOfWork.GameRepository.GetByPropertyValue(g => g.Key == key).FirstOrDefault();
            if (game == null)
                return false;
            _unitOfWork.GameRepository.Delete(game);
            _unitOfWork.Commit();
            return true;

        }

        public bool EditGame(GameDto gameDto)
        {
           
            var gameInDb = _unitOfWork.GameRepository.GetByPropertyValue(g => g.Key == gameDto.Key).FirstOrDefault();
            if (gameInDb == null)
                return false;

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
            return true;
        }

        public IQueryable<GameDto> GetAllGames()
        {
                return  _unitOfWork.GameRepository
                  .Get(includeProperties: "Comments, Genres, PlatformTypes")
                  .ProjectTo<GameDto>();
        }

        public GameDto GetGameByKey(string key)
        {
            var game = _unitOfWork.GameRepository.GetByPropertyValue(g => g.Key == key).FirstOrDefault();
            if (game != null)
                return Mapper.Map<Game, GameDto>(game);
            return null;
        }

    }
}