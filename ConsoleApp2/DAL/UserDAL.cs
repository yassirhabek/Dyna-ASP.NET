using Interfaces.Interface;
using MySql.Data.MySqlClient;

namespace DAL.DAL
{
    public class UserDAL : IUserContainer
    {
        public bool Login(string connString, string email, string hashedPw)
        {
            List<string> errors = new List<string>();
            string queryString = "SELECT * FROM [users] WHERE email = @email AND password = @password;";

            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(queryString, con))
                {
                    cmd.Parameters.Add(new MySqlParameter("@email", email));
                    cmd.Parameters.Add(new MySqlParameter("@password", hashedPw));


                    con.Open();

                    var rdr = cmd.ExecuteReader();

                    rdr.Read();

                    if (rdr.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        errors.Add("incorrect login credentials");
                        return false;
                    }
                }
            }
        }

        public int Register(string connString, string email, string username, string hashedPw)
        {
            string queryString = "INSERT INTO [users] (email, username, password) VALUES (@email, @username, @password);";

            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(queryString, con))
                {
                    con.Open();

                    cmd.Parameters.Add(new MySqlParameter("@email", email.ToLower()));
                    cmd.Parameters.Add(new MySqlParameter("@username", username));
                    cmd.Parameters.Add(new MySqlParameter("@password", hashedPw));

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public bool checkIfEmailExists(string email, string connString)
        {
            string queryString = "SELECT TOP 1 id FROM [users] WHERE email = @email";

            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(queryString, con))
                {
                    cmd.Parameters.Add(new MySqlParameter("@email", email));

                    con.Open();

                    int result = Convert.ToInt32(cmd.ExecuteScalar());

                    if (result == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
    }
}
