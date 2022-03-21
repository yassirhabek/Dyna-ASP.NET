namespace Logic.Models
{
    public class Werknemer
    { 
        public int WerknemerID { get; set; }
        public int NummerPasje { get; set; }
        public string Naam { get; set; }
        public double AantalUren { get; set; }

        public Werknemer()
        {

        }

        public Werknemer(int werknemerID, int nummerPasje, string naam)
        {
            WerknemerID = werknemerID;
            NummerPasje = nummerPasje;
            Naam = naam;
            AantalUren = 0;
        }

        public Werknemer(int nummerPasje, string naam)
        {
            NummerPasje = nummerPasje;
            Naam = naam;
            AantalUren = 0;
        }
    }
}
