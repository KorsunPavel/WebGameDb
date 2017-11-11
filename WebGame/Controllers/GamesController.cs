using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebGame.Data;

namespace WebGame.Controllers
{
    public class GamesController : ApiController
    {
        private DbContext _db;
        public GamesController()
        {
            _db = new DbContext("GamesController");
        }

        // GET api/games
        public IHttpActionResult GetGames()
        {
            return Ok();
        }

    }
}
