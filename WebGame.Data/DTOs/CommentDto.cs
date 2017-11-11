using System.Collections.Generic;
using System;

namespace WebGame.Data
{

    public partial class CommentDto
    {
        public CommentDto()
        {
            this.ChildComments = new HashSet<CommentDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public Nullable<int> Game_Id { get; set; }
        public Nullable<int> ParentCommentId { get; set; }

        public virtual ICollection<CommentDto> ChildComments { get; set; }
        public virtual CommentDto ParentComment { get; set; }
        public virtual GameDto TblGame { get; set; }
    }
}
