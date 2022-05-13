using Interfaces.DTO;
using Interfaces.Interface;
using MySql.Data.MySqlClient;
using System.Data;

namespace DAL.DAL
{
    public class UserDAL : DB, IUserContainer
    {
        public bool Login(UserDTO userDTO)
        {
            List<string> errors = new List<string>();
            string queryString = "SELECT * FROM users WHERE Email = @email AND Password = @password;";

            using (connection)
            {
                using (MySqlCommand cmd = new MySqlCommand(queryString, connection))
                {
                    cmd.Parameters.Add(new MySqlParameter("@email", userDTO.Email));
                    cmd.Parameters.Add(new MySqlParameter("@password", userDTO.HashedPassword));


                    if (openConnection())
                    {

                        var rdr = cmd.ExecuteReader();

                        rdr.Read();
                        

                        if (rdr.HasRows)
                        {
                            closeConnection();
                            return true;
                        }
                        else
                        {
                            closeConnection();
                            errors.Add("incorrect login credentials");
                            return false;
                        }
                    }
                    return false;
                }
            }
        }

        public int Register(UserDTO userDTO)
        {
            string queryString = "INSERT INTO users (Email, UserName, Password) VALUES (@email, @username, @password);";

            using (connection)
            {
                using (MySqlCommand cmd = new MySqlCommand(queryString, connection))
                {
                    if (openConnection())
                    {
                        cmd.Parameters.Add(new MySqlParameter("@email", userDTO.Email.ToLower()));
                        cmd.Parameters.Add(new MySqlParameter("@username", userDTO.UserName));
                        cmd.Parameters.Add(new MySqlParameter("@password", userDTO.HashedPassword));

                        int result = cmd.ExecuteNonQuery();
                        closeConnection();
                        return result;
                    }
                    throw new DataException();
                }
            }
        }

        public bool checkIfEmailExists(string email)
        {
            string queryString = "SELECT Email FROM users WHERE Email = @email LIMIT 1";

            using (connection)
            {
                using (MySqlCommand cmd = new MySqlCommand(queryString, connection))
                {
                    cmd.Parameters.Add(new MySqlParameter("@email", email));

                    if (openConnection())
                    {
                        int result = Convert.ToInt32(cmd.ExecuteScalar());

                        closeConnection();

                        if (result == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        throw new DataException();
                    }
                }
            }
        }
    }
}
