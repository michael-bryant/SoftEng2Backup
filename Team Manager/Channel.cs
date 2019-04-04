using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager
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
