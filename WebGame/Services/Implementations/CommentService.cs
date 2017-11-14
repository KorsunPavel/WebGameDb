//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using GameStore.Web.Models;
//using GameStore.DAL.Interfaces;
//using GameStore.DAL.Entities;

//namespace GameStore.Web.Services.Implementations
//{
//    public class CommentService : Interfaces.ICommentService
//    {
//        private IUnitOfWork _db;

//        public CommentService(IUnitOfWork unitOfWork)
//        {
//            _db = unitOfWork;
//        }

//        /// <summary>
//        /// add new comment
//        /// </summary>
//        /// <param name="comment"></param>
//        /// <returns>false if game or parent comment not found</returns>
//        public bool AddComment(CommentDTO comment)
//        {
//            var game = _db.GetRepository<Game>().Get(comment.GameId);
//            if (game == null)
//            {
//                return false;
//            }
//            if (comment.ParentCommentId.HasValue)
//            {
//                var parentComment = _db.GetRepository<Comment>().Get(comment.ParentCommentId.Value);
//                if (parentComment == null)
//                {
//                    return false;
//                }
//            }
//            _db.GetRepository<Comment>().Add(new Comment
//            {
//                Author = comment.Author,
//                Body = comment.Body,
//                GameId = comment.GameId,
//                ParentCommentId = comment.ParentCommentId
//            });
//            _db.Save();
//            return true;
//        }

//        public IEnumerable<CommentDTO> GetAllGameComments(int gameId)
//        {
//            var game = _db.GetRepository<Game>().Get(gameId);
//            return game?.Comments.Select(c => new CommentDTO
//            {
//                Id = c.Id,
//                Author = c.Author,
//                Body = c.Body,
//                GameId = c.GameId,
//                ParentCommentId = c.ParentCommentId
//            });
//        }
//    }
//}