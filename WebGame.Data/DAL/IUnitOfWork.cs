using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGame.Data.DAL
{
    public interface IUnitOfWork
    {
        IRepository<Game> GameRepository { get; }
        IRepository<Genre> GenreRepository { get; }
        IRepository<PlatformType> PlatformTypeRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        void Commit();
    }
}
