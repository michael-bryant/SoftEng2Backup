using System;
using System.Data.SqlClient;
using System.Data;
using GroupMeUp.Models;
using System.Collections.Generic;

public class DataBaseHandler
{
    string conString;
    SqlConnection con;
    //Gets the authority level of a user
    public role getUserAuth(int userID)
    {
        
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
    public Message[] getMessagesByTeam(int teamID)
    {
        string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Big Boss\\Documents\\SowftwareENGR\\Connection\\Connection\\Database1.mdf;Integrated Security=True";
        SqlConnection con = new SqlConnection(conString);
        con.Open();

        //temp var


        SortedSet<Message> messageSet = new SortedSet<Message>();

        if (con.State == ConnectionState.Open)
        {

            SqlCommand get_message = new SqlCommand("SELECT body,UseruserID,TeamteamID FROM [Messages] WHERE teamID ='"
                + teamID + "'", con);

            SqlDataReader reader = get_message.ExecuteReader();
            while (reader.Read())
            {
                Message temp = new Message();
                temp.text = (String)reader.GetValue(1);
                temp.userID = (int)reader.GetValue(2);
                temp.teamID = (int)reader.GetValue(3);
                messageSet.Add(temp);
            }
        }
        Message[] message = new Message[messageSet.Count];
        return message;
    }

    public void addMessage(Message message)
    {
        string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Big Boss\\Documents\\SowftwareENGR\\Connection\\Connection\\Database1.mdf;Integrated Security=True";
        SqlConnection con = new SqlConnection(conString);
        con.Open();

        if (con.State == ConnectionState.Open)
        {
            String query = "INSERT INTO [Messages](teamID,userID,message) VALUES(@teamID,@userID,@message)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@teamID", message.teamID);
            cmd.Parameters.AddWithValue("@userID", message.userID);
            cmd.Parameters.AddWithValue("@message", message.text);
            cmd.ExecuteNonQuery();
        }

    }
    public DataBaseHandler()
	{
        //Get data base
        conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Big Boss\\Documents\\SowftwareENGR\\Connection\\Connection\\Database1.mdf;Integrated Security=True";
        con = new SqlConnection(conString);
        con.Open();


    }
}
