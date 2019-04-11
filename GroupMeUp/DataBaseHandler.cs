using System;
using System.Data.SqlClient;
using System.Data;

public class DataBaseHandler
{
	public DataBaseHandler()
	{
        //Gets the authority level of a user
        String getUserAuth(int userID)
        {
            //Get data base
            string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Big Boss\\Documents\\SowftwareENGR\\Connection\\Connection\\Database1.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            //temp var
            String position = null;
            char pos;

            if (con.State == ConnectionState.Open)
            {

                SqlCommand user_auth = new SqlCommand("SELECT role FROM [UserTeam] WHERE userID ='"
                    + userID + "'", con);

                pos = (char)user_auth.ExecuteScalar();

                if (pos.Equals('a'))
                    position = "admin";
                else if (pos.Equals('u'))
                    position = "user";
                else if (pos.Equals('o'))
                    position = "owner";
                else
                    position = "error";
                  
            }
            return position;
        }

        //gets all messages for a certain team
        void getMessage(int teamID)
        {
            string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Big Boss\\Documents\\SowftwareENGR\\Connection\\Connection\\Database1.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            //temp var
            String message;

            if (con.State == ConnectionState.Open)
            {

                SqlCommand get_message = new SqlCommand("SELECT body FROM [Messages] WHERE teamID ='"
                    + messageID + "'", con);

                message = (String)get_message.ExecuteScalar();

                /********************
                 *  HOWEVER THE FUCK WE PRINT THE SHIT
                 * *****************/
            }
        }

        void addMessage(String mes, int teamID, int userID)
        {
            string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Big Boss\\Documents\\SowftwareENGR\\Connection\\Connection\\Database1.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            if(con.State == ConnectionState.Open)
            {
                String query = "INSERT INTO [Messages](teamID,userID,message) VALUES(@teamID,@userID,@message)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@teamID", teamID);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@message", message);
                cmd.ExecuteNonQuery();
            }
            
        }
        
	}
}
