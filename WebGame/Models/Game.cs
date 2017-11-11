using System;
using System.Collections.Generic;

namespace WebGame.Data
{

    public partial class Game
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<GenreDto> Genres { get; set; }
        public virtual ICollection<PlatformTypeDto> PlatformTypes { get; set; }
    }
}
