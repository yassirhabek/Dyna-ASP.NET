﻿using Interfaces.DTO;
using Interfaces.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAL
{
    public class WerknemerDAL : DB, IWerknemer, IWerknemerContainer
    {
        public int AddNewWerknemer(WerknemerDTO werknemerNieuw, int userID)
        {
            //string query = "START TRANSACTION;" +
            //    "INSERT INTO werknemers(WerknemerNummer, Naam, Telefoonnummer) VALUES(@numpas, @naam, @telefoonnum); " +
            //    "INSERT INTO userwerknemerlink (UserID, WerknemerID) VALUES (@userid, (SELECT WerknemerID FROM werknemers WHERE Telefoonnummer = @telefoonnum)); " +
            //    "COMMIT;";

            string query = "START TRANSACTION;" +
                "INSERT INTO werknemers(WerknemerNummer, Naam, Telefoonnummer) VALUES(@numpas, @naam, @telefoonnum);" +
                "SELECT LAST_INSERT_ID();" +
                "COMMIT;";

            if (openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("@numpas", MySqlDbType.Int32).Value = werknemerNieuw.WerknemerNummer;
                cmd.Parameters.Add("@naam", MySqlDbType.VarChar).Value = werknemerNieuw.Naam;
                cmd.Parameters.Add("@telefoonnum", MySqlDbType.Int32).Value = werknemerNieuw.TelefoonNummer;
                cmd.Parameters.Add("@userid", MySqlDbType.Int32).Value = userID;

                try
                {
                    int werknemerid = Convert.ToInt32(cmd.ExecuteScalar());
                    WerknemerDTO werknemerDTO = new WerknemerDTO()
                    {
                        WerknemerID = werknemerid,
                    };
                    LinkWerknemerToUser(werknemerDTO, userID);
                }
                catch (MySqlException ex)
                {                    
                    throw new Exception(ex.ToString());
                }
                closeConnection();
                return 1;
            }
            else
            {
                return 0;
                throw new DataException();
            }
        }

        public int UpdateWerknemer(WerknemerDTO changedWerknemer, int oldWerknemerID)
        {
            string query = "UPDATE werknemers SET Naam = @naam, WerknemerNummer = @werknum, Telefoonnummer = @telefoonnum WHERE WerknemerID = @oldwerknemerid";

            if (openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("@naam", MySqlDbType.VarChar).Value = changedWerknemer.Naam;
                cmd.Parameters.Add("@werknum", MySqlDbType.Int32).Value = changedWerknemer.WerknemerNummer;
                cmd.Parameters.Add("@telefoonnum", MySqlDbType.Int32).Value = changedWerknemer.TelefoonNummer;
                cmd.Parameters.Add("@oldwerknemerid", MySqlDbType.Int64).Value = oldWerknemerID;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    return 0;
                    throw new Exception(ex.ToString());
                }
                closeConnection();
                return 1;
            }
            else
            {
                return 0;
                throw new DataException();
            }
                
        }

        public int DeleteWerknemer(WerknemerDTO werknemer)
        {
            string query = "DELETE FROM werknemers WHERE WerknemerID = @werknemerID";

            if (openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("@werknemerID", MySqlDbType.VarChar).Value = werknemer.WerknemerID;

                try
                {
                    cmd.ExecuteNonQuery();
                    return 1;
                }
                catch (MySqlException ex)
                {
                    return 0;
                    throw new Exception(ex.ToString());
                }
            }
            else
            {
                return 0;
                throw new DataException();
            }

        }
        public List<WerknemerDTO> GetAllWerknemers()
        {
            var output = new List<WerknemerDTO>();
            if (openConnection())
            {
                try
                {
                    string query = "SELECT * FROM werknemers";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    MySqlDataReader rdr = cmd.ExecuteReader();


                    while (rdr.Read())
                    {
                        output.Add(new WerknemerDTO()
                        {
                            WerknemerID = Convert.ToInt32(rdr[0]),
                            WerknemerNummer = Convert.ToInt32(rdr[1]),
                            Naam = Convert.ToString(rdr[2]),
                            TelefoonNummer = Convert.ToInt32(rdr[3])
                        });
                    }
                    rdr.Close();
                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.ToString());
                }
                closeConnection();
                return output;
            }
            else
                throw new DataException();
        }

        public List<WerknemerDTO> GetUserWerknemers(int userID)
        {
            var output = new List<WerknemerDTO>();
            if (openConnection())
            {
                try
                {
                    //string query = "SELECT * FROM werknemers WHERE UserID = @userid";
                    string query = "SELECT DISTINCT werknemers.* " +
                        "FROM userwerknemerlink JOIN users on userwerknemerlink.UserID = @userid " +
                        "JOIN werknemers on userwerknemerlink.WerknemerID = werknemers.WerknemerID";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.Add("@userid", MySqlDbType.Int32).Value = userID;

                    MySqlDataReader rdr = cmd.ExecuteReader();


                    while (rdr.Read())
                    {
                        output.Add(new WerknemerDTO()
                        {
                            WerknemerID = Convert.ToInt32(rdr[0]),
                            WerknemerNummer = Convert.ToInt32(rdr[1]),
                            Naam = Convert.ToString(rdr[2]),
                            TelefoonNummer = Convert.ToInt32(rdr[3])
                        });
                    }
                    rdr.Close();
                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.ToString());
                }
                closeConnection();
                return output;
            }
            else
                throw new DataException();
        }

        public WerknemerDTO GetSingleWerknemer(int ID)
        {
            if (openConnection())
            {
                try
                {
                    WerknemerDTO output = new WerknemerDTO();
                    string query = "SELECT * FROM werknemers WHERE WerknemerID = @werknemerID LIMIT 1;";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.Add("@werknemerID", MySqlDbType.Int32).Value = ID;

                    MySqlDataReader rdr = cmd.ExecuteReader();


                    if (rdr.Read())
                    {
                        output = new WerknemerDTO()
                        {
                            WerknemerID = Convert.ToInt32(rdr[0]),
                            WerknemerNummer = Convert.ToInt32(rdr[1]),
                            Naam = Convert.ToString(rdr[2]),
                            TelefoonNummer = Convert.ToInt32(rdr[3])
                        };
                    }
                    rdr.Close();
                    closeConnection();
                    return output;
                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            else
                throw new DataException();
        }

        public int LinkWerknemerToUser(WerknemerDTO werknemer, int userID)
        {
            if (openConnection())
            {
                string query = "INSERT INTO userWerknemerLink (UserID, WerknemerID) VALUES (@userid, @werknemerid)";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("@userid", MySqlDbType.Int32).Value = userID;
                cmd.Parameters.Add("@werknemerid", MySqlDbType.Int32).Value = werknemer.WerknemerID;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                closeConnection();
                return 1;
            }
            else
            {
                throw new DataException();
            }
        }
    }
}