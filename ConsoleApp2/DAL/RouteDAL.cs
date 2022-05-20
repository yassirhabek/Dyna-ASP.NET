using Interfaces.DTO;
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
    public class RouteDAL : DB, IRoute, IRouteContainer
    {
        private const string timespanFormat = "hh:mm";
        public int AddRoute(RouteDTO newRoute, int userID)
        {
            string query = "INSERT INTO route (RouteNummer, Datum, Chauffeur, Bijrijder, Starttijd, Eindtijd, AantalUur, Bijzonderheden, UserID, DatumToegevoegd) " +
                        "VALUES (@route, @date, @chauf, @bijr, @stijd, @etijd, @aanu, @bijz, @userid, @curdate)";

            if (openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("@route", MySqlDbType.Int32).Value = newRoute.RouteNummer;
                cmd.Parameters.Add("@date", MySqlDbType.DateTime).Value = newRoute.Datum;
                cmd.Parameters.Add("@chauf", MySqlDbType.Int32).Value = newRoute.Chauffeur.WerknemerID;
                cmd.Parameters.Add("@bijr", MySqlDbType.Int32).Value = newRoute.BijRijder.WerknemerID;
                cmd.Parameters.Add("@stijd", MySqlDbType.VarChar).Value = newRoute.StartTijd;
                cmd.Parameters.Add("@etijd", MySqlDbType.VarChar).Value = newRoute.EindTijd;
                cmd.Parameters.Add("@aanu", MySqlDbType.VarChar).Value = newRoute.AantalUur;
                cmd.Parameters.Add("@bijz", MySqlDbType.Text).Value = newRoute.Bijzonderheden;
                cmd.Parameters.Add("@userid", MySqlDbType.VarChar).Value = userID;
                cmd.Parameters.Add("@curdate", MySqlDbType.DateTime).Value = DateTime.Now;

                try
                {
                    cmd.ExecuteNonQuery();
                    closeConnection();
                    return 1;
                }
                catch (MySqlException ex)
                {
                    closeConnection();
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

        public List<RouteDTO> GetRouteByDate(DateTime date, List<WerknemerDTO> lijstWerknemers, int userID)
        {
            List<RouteDTO> output = new List<RouteDTO>();

            if (openConnection())
            {
                string query = "SELECT * FROM route WHERE Datum= @datum AND UserID= @userid;";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("@datum", MySqlDbType.Date).Value = date;
                cmd.Parameters.Add("@userid", MySqlDbType.Int32).Value = userID;

                MySqlDataReader rdr = cmd.ExecuteReader();

                try
                {
                    while (rdr.Read())
                    {
                        output.Add(new RouteDTO()
                        {
                            RouteID = Convert.ToInt32(rdr[0]),
                            RouteNummer = Convert.ToInt32(rdr[1]),
                            Datum = Convert.ToDateTime(rdr[2]),
                            Chauffeur = lijstWerknemers.FirstOrDefault(w => w.WerknemerID == Convert.ToInt32(rdr[3])),
                            BijRijder = lijstWerknemers.FirstOrDefault(w => w.WerknemerID == Convert.ToInt32(rdr[4])),
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
        public List<RouteDTO> GetAllRoute(List<WerknemerDTO> lijstWerknemers, int userID)
        {
            List<RouteDTO> output = new List<RouteDTO>();

            if (openConnection())
            {
                string query = "SELECT * FROM route WHERE UserID= @userid";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                
                cmd.Parameters.Add("@userid", MySqlDbType.Int32).Value = userID;

                MySqlDataReader rdr = cmd.ExecuteReader();

                try
                {
                    while (rdr.Read())
                    {
                        output.Add(new RouteDTO()
                        {
                            RouteID = Convert.ToInt32(rdr[0]),
                            RouteNummer = Convert.ToInt32(rdr[1]),
                            Datum = Convert.ToDateTime(rdr[2]),
                            Chauffeur = lijstWerknemers.FirstOrDefault(w => w.WerknemerID == Convert.ToInt32(rdr[3])),
                            BijRijder = lijstWerknemers.FirstOrDefault(w => w.WerknemerID == Convert.ToInt32(rdr[4])),
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

        public List<RouteDTO> GetRoutesByMonth(DateTime smalldate, DateTime bigdate, List<WerknemerDTO> lijstWerknemers, int userID)
        {
            List<RouteDTO> routes = new List<RouteDTO>();

            if (openConnection())
            {
                string query = "SELECT * FROM route WHERE Datum >= @smalldate AND Datum < @bigdate AND UserID= @usid";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add("@smalldate", MySqlDbType.DateTime).Value = smalldate;
                cmd.Parameters.Add("@bigdate", MySqlDbType.DateTime).Value = bigdate;
                cmd.Parameters.Add("@usid", MySqlDbType.Int32).Value = userID;

                MySqlDataReader rdr = cmd.ExecuteReader();

                try
                {
                    while (rdr.Read())
                    {
                        RouteDTO routeDTO = new RouteDTO()
                        {
                            RouteID = Convert.ToInt32(rdr[0]),
                            RouteNummer = Convert.ToInt32(rdr[1]),
                            Datum = Convert.ToDateTime(rdr[2]),
                            Chauffeur = lijstWerknemers.FirstOrDefault(w => w.WerknemerID == Convert.ToInt32(rdr[3])),
                            BijRijder = lijstWerknemers.FirstOrDefault(w => w.WerknemerID == Convert.ToInt32(rdr[4])),
                            StartTijd = TimeSpan.Parse(Convert.ToString(rdr[5])),
                            EindTijd = TimeSpan.Parse(Convert.ToString(rdr[6])),
                            AantalUur = TimeSpan.Parse(Convert.ToString(rdr[7])),
                            Bijzonderheden = Convert.ToString(rdr[8])
                        };
                        routes.Add(routeDTO);
                    }
                    rdr.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                closeConnection();
                return routes;
            }
            else
                throw new DataException();
        }

        public int UpdateRoute(RouteDTO updateRoute, int oldRouteID)
        {

            string query = "UPDATE route SET RouteNummer= @newroutenum, Datum= @datum, Chauffeur= @chauff, Bijrijder= @bijr," +
                "Starttijd= @startt, Eindtijd= @eindt, AantalUur= @aantaluur, Bijzonderheden =@bijz, DatumToegevoegd= @datumtoeg WHERE routeID = @routeID";

            if (openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("@newroutenum", MySqlDbType.Int32).Value = updateRoute.RouteNummer;
                cmd.Parameters.Add("@datum", MySqlDbType.Text).Value = updateRoute.Datum;
                cmd.Parameters.Add("@chauff", MySqlDbType.Text).Value = updateRoute.Chauffeur.Naam;
                cmd.Parameters.Add("@bijr", MySqlDbType.Text).Value = updateRoute.BijRijder.Naam;
                cmd.Parameters.Add("@startt", MySqlDbType.Time).Value = updateRoute.StartTijd;
                cmd.Parameters.Add("@eindt", MySqlDbType.Time).Value = updateRoute.EindTijd;
                cmd.Parameters.Add("@aantaluur", MySqlDbType.Time).Value = updateRoute.AantalUur;
                cmd.Parameters.Add("@bijz", MySqlDbType.Text).Value = updateRoute.Bijzonderheden;
                cmd.Parameters.Add("@datumtoeg", MySqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@routeid", MySqlDbType.Int32).Value = oldRouteID;

                try
                {
                    cmd.ExecuteNonQuery();
                    closeConnection();
                    return 1;
                }
                catch (MySqlException ex)
                {
                    closeConnection();
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

        public int DeleteRoute(RouteDTO deleteRoute)
        {
            string query = "DELETE FROM route WHERE RouteID = @routeid";

            if (openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("@routeid", MySqlDbType.Int64).Value = deleteRoute.RouteID;

                try
                {
                    cmd.ExecuteNonQuery();
                    closeConnection();
                    return 1;
                }
                catch (MySqlException ex)
                {
                    closeConnection();
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
    }
}
