using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Data;
using System.TimeSpan;

public class DataBaseHandler
{
    public DataBaseHandler()
    {

    }

    //Methods to handle the DB

    //*******************************************************************************************************************
    //User

        //Gets the authority level of a user
        String getUserAuth(int userID)
        {
            //Get data base
            string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Big Boss\\Documents\\SowftwareENGR\\Connection\\Connection\\Database1.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            //temp var
            String position;
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

        //add user
        void addUser(String uN, String pass, time b, time e)
        {
            string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Big Boss\\Documents\\SowftwareENGR\\Connection\\Connection\\Database1.mdf;Integrated Security=True";
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
        void addTeam(String tN, time b, time e)
        {
            string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Big Boss\\Documents\\SowftwareENGR\\Connection\\Connection\\Database1.mdf;Integrated Security=True";
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
            string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Big Boss\\Documents\\SowftwareENGR\\Connection\\Connection\\Database1.mdf;Integrated Security=True";
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
                    + teamID + "'", con);

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
                cmd.Parameters.AddWithValue("@message", mess);
                cmd.ExecuteNonQuery();
            }
            
        }


    //***************************************************************************************************************
    //Time
        
        //for the meet up times
        void updateTimeFrame(int teamID)
        {
            string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Big Boss\\Documents\\SowftwareENGR\\Connection\\Connection\\Database1.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                time eT;
                time bT;
                //begin
                SqlCommand get_bTime = new SqlCommand("SELECT MAX(beggining) as bTime FROM User, Team, UserOnTeam " +
                    "WHERE User.userID = UserOnTeam.userID AND UserOnTeam.teamID = Team." + teamID + "", con);

                bT = (time)get_bTime.ExecuteScalar();

                //end
                SqlCommand get_eTime = new SqlCommand("SELECT MIN(end) as eTime FROM User, Team, UserOnTeam " +
                    "WHERE User.userID = UserOnTeam.userID AND UserOnTeam.teamID = Team." + teamID + "", con);

                eT = (time)get_eTime.ExecuteScalar();

                //Update
                String query = "UPDATE Team SET begining = @beginning, end = @end FROM Team WHERE teamID = " + teamID + "";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@begging", bT);
                cmd.Parameters.AddWithValue("@end", eT);
                cmd.ExecuteNonQuery();

            }
        }

        //get a teams beggining meet up time
        time getBegginingTime(int teamID)
        {
            string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Big Boss\\Documents\\SowftwareENGR\\Connection\\Connection\\Database1.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                SqlCommand get_bTime = new SqlCommand("SELECT beggining FROM Team " +
                    "WHERE teamID = " + teamID + "", con);

               return (time)get_bTime.ExecuteScalar();
            }
        }

        //get the end of a teams meet up time
        time getEndTime(int teamID)
        {
            string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Big Boss\\Documents\\SowftwareENGR\\Connection\\Connection\\Database1.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                SqlCommand get_eTime = new SqlCommand("SELECT end FROM Team " +
                    "WHERE teamID = " + teamID + "", con);

                return (time)get_eTime.ExecuteScalar();
            }
        }
}
