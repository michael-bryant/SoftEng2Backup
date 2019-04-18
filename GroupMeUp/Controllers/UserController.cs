using GroupMeUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GroupMeUp.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        DataBaseHandler dbh = new DataBaseHandler();
        [Route("")]
        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            return null;
        }

        [Route("{userID:int}")]
        [HttpGet]
        public User GetByID(int userID)
        {
            return null;
        }

        [Route("{userID:int}/send/{teamID:int}/{message}")]
        [HttpGet]
        public void sendMessage(int userID, int teamID, String message)
        {
            Message newMessage = new Message();
            newMessage.userID = userID;
            newMessage.teamID = teamID;
            newMessage.text = message;
            dbh.addMessage(newMessage);
        }
    }
}
