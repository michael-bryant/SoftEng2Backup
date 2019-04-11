using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupMeUp.Models
{
    class Channel
    {
        SortedList<int,Message> messages;

        public Channel()
        {
            messages = new SortedList<int, Message>();
        }

        public void retrieveMessages()
        {

        }
    }
}
