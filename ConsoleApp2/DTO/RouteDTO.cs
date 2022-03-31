using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public struct RouteDTO
    {
        public int RouteID;
        public int RouteNummer;
        public DateTime Datum;
        public WerknemerDTO Chauffeur;
        public WerknemerDTO BijRijder;
        public TimeSpan StartTijd;
        public TimeSpan EindTijd;
        public TimeSpan AantalUur;
        public string Bijzonderheden;
    }
}
