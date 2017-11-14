using System.Collections.Generic;
using System.Linq;
using WebGame.Data;

namespace GameStore.Web.Services.Interfaces
{
    public interface IGameService
    {
        GameDto CreateNewGame(GameDto game);

        bool EditGame(GameDto game);

        bool DeleteGame(string key);

        GameDto GetGameByKey(string key);

        IQueryable<GameDto> GetAllGames();

    }
}
