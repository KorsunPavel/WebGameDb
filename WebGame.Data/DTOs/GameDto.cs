using System;
using System.Collections.Generic;

namespace WebGame.Data
{

    public partial class GameDto
    {
        public GameDto()
        {
            this.TblComments = new HashSet<CommentDto>();
            this.TblGenres = new HashSet<GenreDto>();
            this.TblPlatformTypes = new HashSet<PlatformTypeDto>();
        }

        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CommentDto> TblComments { get; set; }
        public virtual ICollection<GenreDto> TblGenres { get; set; }
        public virtual ICollection<PlatformTypeDto> TblPlatformTypes { get; set; }
    }
}
