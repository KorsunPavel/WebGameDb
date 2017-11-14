using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGame.Data;

namespace GameStore.Web.Services.Interfaces
{
    public interface IGameFilterService
    {
        IEnumerable<GameDto> GetGamesByGenre(GenreDto genre);

        IEnumerable<GameDto> GetGamesByPlatform(PlatformTypeDto platform);
    }
}
