using System.Collections.Generic;
using System;

namespace WebGame.Data
{

    public partial class Comment
    {
        public Comment()
        {
            this.ChildComments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        //public Nullable<int> GameId { get; set; }
        public Nullable<int> ParentCommentId { get; set; }

        public virtual ICollection<Comment> ChildComments { get; set; }
        public virtual Comment ParentComment { get; set; }
        public virtual Game Game { get; set; }
    }
}
