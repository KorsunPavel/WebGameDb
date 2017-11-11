using System;
using System.Collections.Generic;

namespace WebGame.Data
{

    public partial class GenreDto
    {
        public GenreDto()
        {
            this.ChildGenres = new HashSet<GenreDto>();
            this.TblGames = new HashSet<GameDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentGenreId { get; set; }

        public virtual ICollection<GenreDto> ChildGenres { get; set; }
        public virtual GenreDto ParentGenre { get; set; }
        public virtual ICollection<GameDto> TblGames { get; set; }
    }
}
