using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupMeUp.Models
{
    public class Channel
    {
        private SortedList<int,Message> messages;

        public Channel()
        {
            messages = new SortedList<int, Message>();
        }

        public void retrieveMessages()
        {

        }
    }
}
