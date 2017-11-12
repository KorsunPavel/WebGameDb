using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebGame.Data
{

    public partial class Genre
    {
        public Genre()
        {
            this.ChildGenres = new HashSet<Genre>();
            this.Games = new HashSet<Game>();
        }

        public int Id { get; set; }

        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        public Nullable<int> ParentGenreId { get; set; }

        public virtual ICollection<Genre> ChildGenres { get; set; }
        public virtual Genre ParentGenre { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
