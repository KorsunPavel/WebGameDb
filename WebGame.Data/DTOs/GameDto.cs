using System;
using System.Collections.Generic;

namespace WebGame.Data
{

    public partial class GameDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<string> Genres { get; set; }
        public virtual ICollection<string> PlatformTypes { get; set; }
    }
}
