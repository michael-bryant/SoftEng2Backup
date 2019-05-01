using System;
using System.Data.SqlClient;
using System.Data;
using GroupMeUp.Models;
using System.Collections.Generic;

public class DataBaseHandler
{
    string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Big Boss\\Documents\\SowftwareENGR\\Connection\\Connection\\Database1.mdf;Integrated Security=True";
    public DataBaseHandler()
    {

    }

    //Methods to handle the DB

    //*******************************************************************************************************************
    //User

        //Gets the authority level of a user
        GroupMeUp.Models.role getUserAuth(int userID)
        {
            //Get data base
            SqlConnection con = new SqlConnection(conString);
            con.Open();

        //temp var
             GroupMeUp.Models.role position = GroupMeUp.Models.role.USER;
            char pos;

            if (con.State == ConnectionState.Open)
            {

                SqlCommand user_auth = new SqlCommand("SELECT role FROM [UserTeam] WHERE userID ='"
                    + userID + "'", con);

                pos = (char)user_auth.ExecuteScalar();

            if (pos.Equals('a'))
                position = GroupMeUp.Models.role.ADMIN;
            else if (pos.Equals('u'))
                position = GroupMeUp.Models.role.USER ;
                else if (pos.Equals('o'))
                    position = GroupMeUp.Models.role.OWNER;
                else
                    position = GroupMeUp.Models.role.USER;
                  
            }
                return position;
        }

        //add user
        void addUser(String uN, String pass, TimeSpan b, TimeSpan e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                String query = "INSERT INTO [User](username,password,beginning, end) VALUES(@username,@password,@beggining, @end)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@username", uN);
                cmd.Parameters.AddWithValue("@password", pass);
                cmd.Parameters.AddWithValue("@beggining", b);
                cmd.Parameters.AddWithValue("@end", e);
                cmd.ExecuteNonQuery();
            }
        }



    //*******************************************************************************************************************
    //Team

        //add team
        void addTeam(String tN, TimeSpan b, TimeSpan e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                String query = "INSERT INTO [Team](teamName,beginning, end) VALUES(@teamName,@beggining, @end)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@teamName", tN);
                cmd.Parameters.AddWithValue("@beggining", b);
                cmd.Parameters.AddWithValue("@end", e);
                cmd.ExecuteNonQuery();
            }
        }

        // add user on team
        void addUserOnTeam(int uID, int tID, String role)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                String query = "INSERT INTO [UserOnTeam](userID,teamID,role) VALUES(@userID,@teamID,@role)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@userID", uID);
                cmd.Parameters.AddWithValue("@teamID", tID);
                cmd.Parameters.AddWithValue("@role", role);
                cmd.ExecuteNonQuery();
            }
        }



    //*****************************************************************************************************************
    //Messages

    //gets all messages for a certain team
    Message[] getMessage(int teamID)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            //temp var
            SqlDataReader messageReader;

        if (con.State == ConnectionState.Open)
        {

            SqlCommand get_message = new SqlCommand("SELECT * FROM [Messages] WHERE teamID ='"
                + teamID + "'", con);


            messageReader = get_message.ExecuteReader();

            /********************
             *  HOWEVER THE FUCK WE PRINT THE SHIT
             * *****************/
            SortedSet<Message> messages = new SortedSet<Message>();
            while (messageReader.Read())
            {
                Message newMessage = new Message();
                newMessage.text = messageReader.GetString(2);
            }
            Message[] retMessages = new Message[messages.Count];
            messages.CopyTo(retMessages);
            return retMessages;
        }
        else return null;
        }

        void addMessage(String mes, int teamID, int userID)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            if(con.State == ConnectionState.Open)
            {
                String query = "INSERT INTO [Messages](teamID,userID,message) VALUES(@teamID,@userID,@message)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@teamID", teamID);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@message", mes);
                cmd.ExecuteNonQuery();
            }
            
        }


    //***************************************************************************************************************
    //Time
        
        //for the meet up times
        void updateTimeFrame(int teamID)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                TimeSpan eT;
                TimeSpan bT;
                //begin
                SqlCommand get_bTime = new SqlCommand("SELECT MAX(beggining) as bTime FROM User, Team, UserOnTeam " +
                    "WHERE User.userID = UserOnTeam.userID AND UserOnTeam.teamID = Team." + teamID + "", con);

                bT = (TimeSpan)get_bTime.ExecuteScalar();

                //end
                SqlCommand get_eTime = new SqlCommand("SELECT MIN(end) as eTime FROM User, Team, UserOnTeam " +
                    "WHERE User.userID = UserOnTeam.userID AND UserOnTeam.teamID = Team." + teamID + "", con);

                eT = (TimeSpan)get_eTime.ExecuteScalar();

                //Update
                String query = "UPDATE Team SET begining = @beginning, end = @end FROM Team WHERE teamID = " + teamID + "";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@begging", bT);
                cmd.Parameters.AddWithValue("@end", eT);
                cmd.ExecuteNonQuery();

            }
        }

        //get a teams beggining meet up time
        TimeSpan getBeginningTime(int teamID)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                SqlCommand get_bTime = new SqlCommand("SELECT beggining FROM Team " +
                    "WHERE teamID = " + teamID + "", con);

               return (TimeSpan)get_bTime.ExecuteScalar();
            }
        return new TimeSpan();
        }

        //get the end of a teams meet up time
        TimeSpan getEndTime(int teamID)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                SqlCommand get_eTime = new SqlCommand("SELECT end FROM Team " +
                    "WHERE teamID = " + teamID + "", con);

                return (TimeSpan)get_eTime.ExecuteScalar();
            }
        return new TimeSpan();
        }
}
