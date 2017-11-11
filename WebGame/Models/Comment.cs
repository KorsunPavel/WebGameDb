using System.Collections.Generic;
using System;

namespace WebGame.Data
{

    public partial class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public int? Game_Id { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
