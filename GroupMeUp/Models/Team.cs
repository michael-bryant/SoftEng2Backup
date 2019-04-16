using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupMeUp.Models
{
    public class Team
    {
        public SortedSet<Channel> channels;
        public SortedSet<User> users;
    }
}
