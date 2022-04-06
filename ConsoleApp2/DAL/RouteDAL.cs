using Interfaces.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAL
{
    public class RouteDAL : DB
    {
        public void AddRoute(RouteDTO newRoute)
        {
            string query = "INSERT INTO route (RouteNummer, Datum, Chauffeur, Bijrijder, Starttijd, Eindtijd, AantalUur, Bijzonderheden, DatumToegevoegd) " +
                        "VALUES (@route, @date, @chauf, @bijr, @stijd, @etijd, @aanu, @bijz, @curdate)";

            if (openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("@route", MySqlDbType.Int32).Value = newRoute.RouteNummer;
                cmd.Parameters.Add("@date", MySqlDbType.Date).Value = newRoute.Datum;
                cmd.Parameters.Add("@chauf", MySqlDbType.Text).Value = newRoute.Chauffeur.Naam;
                cmd.Parameters.Add("@bijr", MySqlDbType.Text).Value = newRoute.BijRijder.Naam;
                cmd.Parameters.Add("@stijd", MySqlDbType.Time).Value = newRoute.StartTijd;
                cmd.Parameters.Add("@etijd", MySqlDbType.Time).Value = newRoute.EindTijd;
                cmd.Parameters.Add("@aanu", MySqlDbType.Time).Value = newRoute.AantalUur;
                cmd.Parameters.Add("@bijz", MySqlDbType.Text).Value = newRoute.Bijzonderheden;
                cmd.Parameters.Add("@curdate", MySqlDbType.DateTime).Value = DateTime.Now;

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

        public List<RouteDTO> GetRouteFromDate(string date, List<WerknemerDTO> lijstWerknemers)
        {
            List<RouteDTO> output = new List<RouteDTO>();

            string searchDate = date.Remove(10);

            if (openConnection())
            {
                string query = "SELECT * FROM route WHERE Datum=@datum";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("@datum", MySqlDbType.Text).Value = searchDate;

                MySqlDataReader rdr = cmd.ExecuteReader();

                try
                {
                    while (rdr.Read())
                    {
                        output.Add(new RouteDTO()
                        {
                            RouteNummer = Convert.ToInt32(rdr[1]),
                            Datum = Convert.ToDateTime(rdr[2]),
                            Chauffeur = lijstWerknemers.FirstOrDefault(w => w.Naam == rdr[3].ToString()),
                            BijRijder = lijstWerknemers.FirstOrDefault(w => w.Naam == rdr[4].ToString()),
                            StartTijd = TimeSpan.Parse(Convert.ToString(rdr[5])),
                            EindTijd = TimeSpan.Parse(Convert.ToString(rdr[6])),
                            AantalUur = TimeSpan.Parse(Convert.ToString(rdr[7])),
                            Bijzonderheden = Convert.ToString(rdr[8])
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
        public List<RouteDTO> GetAllRoute(List<WerknemerDTO> lijstWerknemers)
        {
            List<RouteDTO> output = new List<RouteDTO>();

            if (openConnection())
            {
                string query = "SELECT * FROM route";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader rdr = cmd.ExecuteReader();

                try
                {
                    while (rdr.Read())
                    {
                        output.Add(new RouteDTO()
                        {
                            RouteNummer = Convert.ToInt32(rdr[1]),
                            Datum = Convert.ToDateTime(rdr[2]),
                            Chauffeur = lijstWerknemers.FirstOrDefault(w => w.Naam == rdr[3].ToString()),
                            BijRijder = lijstWerknemers.FirstOrDefault(w => w.Naam == rdr[4].ToString()),
                            StartTijd = TimeSpan.Parse(Convert.ToString(rdr[5])),
                            EindTijd = TimeSpan.Parse(Convert.ToString(rdr[6])),
                            AantalUur = TimeSpan.Parse(Convert.ToString(rdr[7])),
                            Bijzonderheden = Convert.ToString(rdr[8])
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
        public void UpdateRoute(RouteDTO updateRoute, RouteDTO oldRoute)
        {
            string oldDateFormat = oldRoute.Datum.ToString().Remove(10);
            string newDateFormat = updateRoute.Datum.ToString().Remove(10);

            string query = "UPDATE route SET RouteNummer= @newroutenum, Datum= @datum, Chauffeur= @chauff, Bijrijder= @bijr," +
                "Starttijd= @startt, Eindtijd= @eindt, AantalUur= @aantaluur, Bijzonderheden =@bijz, DatumToegevoegd= @datumtoeg WHERE routeID = @routeID";

            if (openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("@newroutenum", MySqlDbType.Int32).Value = updateRoute.RouteNummer;
                cmd.Parameters.Add("@datum", MySqlDbType.Text).Value = newDateFormat;
                cmd.Parameters.Add("@chauff", MySqlDbType.Text).Value = updateRoute.Chauffeur.Naam;
                cmd.Parameters.Add("@bijr", MySqlDbType.Text).Value = updateRoute.BijRijder.Naam;
                cmd.Parameters.Add("@startt", MySqlDbType.Time).Value = updateRoute.StartTijd;
                cmd.Parameters.Add("@eindt", MySqlDbType.Time).Value = updateRoute.EindTijd;
                cmd.Parameters.Add("@aantaluur", MySqlDbType.Time).Value = updateRoute.AantalUur;
                cmd.Parameters.Add("@bijz", MySqlDbType.Text).Value = updateRoute.Bijzonderheden;
                cmd.Parameters.Add("@datumtoeg", MySqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@routeid", MySqlDbType.Int32).Value = oldRoute.RouteID;

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

        public void DeleteRoute(RouteDTO deleteRoute)
        {
            string query = "DELETE FROM route WHERE RouteID = @routeid";

            if (openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("@routeid", MySqlDbType.Int64).Value = deleteRoute.RouteID;

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
    }
}
