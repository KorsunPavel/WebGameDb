using System;
using System.Collections.Generic;

namespace WebGame.Data
{

    public partial class GenreDto
    {
        public GenreDto()
        {
            this.ChildGenres = new HashSet<string>();
            this.Games = new HashSet<GameDto>();
        }

        public string Name { get; set; }
        public virtual ICollection<string> ChildGenres { get; set; }
        public virtual string ParentGenre { get; set; }
        public virtual ICollection<GameDto> Games { get; set; }
    }
}
