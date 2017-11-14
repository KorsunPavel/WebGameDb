using System.Collections.Generic;
using WebGame.Data;

namespace GameStore.Web.Services.Interfaces
{
    public interface ICommentService
    {
        bool AddComment(CommentDto comment);

        IEnumerable<CommentDto> GetAllGameComments(int gameId);
    }
}
