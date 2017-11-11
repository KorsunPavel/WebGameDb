using System;
using System.Collections.Generic;

namespace WebGame.Data
{

    public partial class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentGenreId { get; set; }

        public virtual ICollection<GenreDto> ChildGenres { get; set; }
        public virtual GenreDto ParentGenre { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
