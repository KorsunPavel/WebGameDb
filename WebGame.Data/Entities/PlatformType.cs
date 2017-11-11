using System;
using System.Collections.Generic;

namespace WebGame.Data
{

    public partial class PlatformType
    {
        public PlatformType()
        {
            this.Games = new HashSet<Game>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
