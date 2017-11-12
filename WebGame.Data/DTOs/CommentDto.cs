using System.Collections.Generic;
using System;

namespace WebGame.Data
{

    public partial class CommentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }

        public int? ParentCommentId { get; set; }
        public virtual int GameId { get; set; }
    }
}
