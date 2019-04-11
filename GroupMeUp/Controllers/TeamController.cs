using GroupMeUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GroupMeUp.Controllers
{
    [RoutePrefix("api/teams")]
    public class TeamController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IEnumerable<Team> GetAllTeams()
        {
            return null;
        }

        [Route("{teamID:int}")]
        [HttpGet]
        public Team GetByID(int teamID)
        {
            return null;
        }
    }
}
