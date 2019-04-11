using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupMeUp.Models
{
    enum role { USER,ADMIN,OWNER}

    class User
    {
        int userID;
        string username;
        int sessionID;
        role permission;
        private static int nextID = 1;
        long ttl;

        private int authenticate(string username, string password)
        {
            //Placeholder
            userID = 13;
            permission = role.USER;
            //*Placeholder
            return nextID++;
        }

        public User(string username,string password)
        {
            if ((sessionID=authenticate(username, password)) == 0)
            {
                Exception e = new Exception("Invalid password");
                throw (e);
            }else
            {
                this.username = username;
                ttl = 3600000;
            }
        }
    }
}
