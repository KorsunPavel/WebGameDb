using System;
using System.Collections.Generic;

namespace WebGame.Data
{

    public partial class PlatformTypeDto
    {
        public PlatformTypeDto()
        {
            this.Games = new HashSet<GameDto>();
        }

        public string Type { get; set; }

        public virtual ICollection<GameDto> Games { get; set; }
    }
}
