using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebGame.Data;
using WebGame.Data.DAL;

namespace WebGame.Controllers
{
    [RoutePrefix("game/{gameId}")]
    public class CommentsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentsController()
        {
            _unitOfWork = new UnitOfWork();
        }

        // GET game/{gameId}/comments
        // User can get all comments by game key (POST URL: /game/{gamekey}/comments).
        [HttpGet]
        [Route("comments")]
        public IHttpActionResult GetAllComments(int gameId)
        {
            var comments = _unitOfWork.CommentRepository
                .Get( c => c.GameId == gameId)
                .ProjectTo<CommentDto>();
            return Ok(comments);
        }

        // POST game/{gameId}/newcomment
        // User can leave comment for game (POST URL: /game/{gamekey}/newcomment).
        [HttpPost]
        [Route("newcomment")]
        public IHttpActionResult AddNewComment([FromBody] CommentDto commentDto)
        {
            var comment = Mapper.Map<Comment>(commentDto);
            _unitOfWork.CommentRepository
                .Insert(comment);
            _unitOfWork.Commit();
            return Ok(comment);
        }
    }
}
