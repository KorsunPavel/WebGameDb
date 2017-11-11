using System;
using System.Collections.Generic;

namespace WebGame.Data
{

    public partial class PlatformTypeDto
    {
        public PlatformTypeDto()
        {
            this.TblGames = new HashSet<GameDto>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<GameDto> TblGames { get; set; }
    }
}
