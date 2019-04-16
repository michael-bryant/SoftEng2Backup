using System;
using System.Data.SqlClient;
using System.Data;
using GroupMeUp.Models;

public class DataBaseHandler
{
	public DataBaseHandler()
	{
        //Gets the authority level of a user
        role getUserAuth(int userID)
        {
            //Get data base
            string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Big Boss\\Documents\\SowftwareENGR\\Connection\\Connection\\Database1.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            
            //temp var
            role position = role.USER;
            char pos;

            if (con.State == ConnectionState.Open)
            {

                SqlCommand user_auth = new SqlCommand("SELECT role FROM [UserTeam] WHERE userID ='"
                    + userID + "'", con);

                pos = (char)user_auth.ExecuteScalar();

                if (pos.Equals('a'))
                    position = role.ADMIN;
                else if (pos.Equals('u'))
                    position = role.USER;
                else if (pos.Equals('o'))
                    position = role.OWNER;
                else
                    position = role.USER;
                  
            }
            return position;
        }

        //gets all messages for a certain team
        Message getMessage(int teamID)
        {
            string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Big Boss\\Documents\\SowftwareENGR\\Connection\\Connection\\Database1.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            //temp var
            Message message = new Message();

            if (con.State == ConnectionState.Open)
            {

                SqlCommand get_message = new SqlCommand("SELECT body,UseruserID,TeamteamID FROM [Messages] WHERE teamID ='"
                    + teamID + "'", con);

                SqlDataReader reader = get_message.ExecuteReader();
                message.text = (String)reader.GetValue(1);
                message.userID = (int)reader.GetValue(2);
                message.teamID = (int)reader.GetValue(3);
            }
            return message;
        }

        void addMessage(Message message)
        {
            string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Big Boss\\Documents\\SowftwareENGR\\Connection\\Connection\\Database1.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            if(con.State == ConnectionState.Open)
            {
                String query = "INSERT INTO [Messages](teamID,userID,message) VALUES(@teamID,@userID,@message)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@teamID", message.teamID);
                cmd.Parameters.AddWithValue("@userID", message.userID);
                cmd.Parameters.AddWithValue("@message", message.text);
                cmd.ExecuteNonQuery();
            }
            
        }
        
	}
}
