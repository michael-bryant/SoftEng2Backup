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
        DataBaseHandler dbh = new DataBaseHandler();
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

        [Route("messages/{teamID:int}")]
        [HttpGet]
        public IEnumerable<Message> GetMessages(int teamID)
        {
            return dbh.getMessagesByTeam(teamID);
        }
    }
}
