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
    }
}
