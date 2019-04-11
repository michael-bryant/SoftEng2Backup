using GroupMeUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GroupMeUp.Controllers
{
    public class MessageController : ApiController
    {
        [Route("Message/TestMessage")]
        public Message Get()
        {
            Message message = new Message();
            message.text = "This is a test message.";
            message.userID = 0;
            message.time = DateTime.Now;
            return message;
        }
    }
}