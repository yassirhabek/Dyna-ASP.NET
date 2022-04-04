using DAL.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAL
{
    public class WerknemerDAL : DB
    {
        public void AddNewWerknemer(WerknemerDTO werknemerNieuw)
        {
            string query = "INSERT INTO werknemers(NummerPasje, Naam, Telefoonnummer) VALUES(@numpas, @naam, @telefoonnum)";

            if (openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("@numpas", MySqlDbType.Int32).Value = werknemerNieuw.NummerPasje;
                cmd.Parameters.Add("@naam", MySqlDbType.Text).Value = werknemerNieuw.Naam;
                cmd.Parameters.Add("@telefoonnum", MySqlDbType.Int64).Value = werknemerNieuw.TelefoonNummer;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.ToString());
                }
                closeConnection();
            }
            else
                throw new DataException();
        }

        public void ChangeWerknemerData(WerknemerDTO changedWerknemer, string newName)
        {
            string query = "UPDATE werknemers SET Naam = @naam, NummerPasje = @numpas WHERE Naam = @snaam";

            if (openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("@naam", MySqlDbType.Text).Value = changedWerknemer.Naam;
                cmd.Parameters.Add("@numpas", MySqlDbType.Int32).Value = changedWerknemer.WerknemerID;
                cmd.Parameters.Add("@snaam", MySqlDbType.Text).Value = newName;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.ToString());
                }
                closeConnection();
            }
            else
                throw new DataException();
        }

        public void DeleteWerknemer(WerknemerDTO werknemer)
        {
            string query = "DELETE FROM werknemers WHERE WerknemerID = @werknemerID";

            if (openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("@werknemerID", MySqlDbType.Text).Value = werknemer.WerknemerID;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            else
                throw new DataException();
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
                            NummerPasje = Convert.ToInt32(rdr[1]),
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
        
    }
}