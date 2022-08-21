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
    public class MonthConclusionDAL : DB, IMonthConclusionContainer
    {
        public List<RouteDTO> GetRoutesByMonth(DateTime smalldate, DateTime bigdate, List<WerknemerDTO> lijstWerknemers, int userID)
        {
            List<RouteDTO> routes = new List<RouteDTO>();

            if (openConnection())
            {
                string query = "SELECT * FROM route WHERE Datum >= @smalldate AND Datum < @bigdate AND UserID= @usid ORDER BY Datum DESC";
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
    }
}
