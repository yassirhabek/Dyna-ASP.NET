﻿using Interfaces.DTO;
using Interfaces.Interface;

namespace Logic.Models
{
    public class Werknemer 
    {
        private IWerknemer _iWerknemer;
        public int WerknemerID { get; set; }
        public int WerknemerNummer { get; set; }
        public string Naam { get; set; }
        public int TelefoonNummer { get; set; }

        public Werknemer(IWerknemer iWerknemer)
        {
            _iWerknemer = iWerknemer;
        }

        public Werknemer(WerknemerDTO dto, IWerknemer iWerknemer)
        {
            WerknemerID = dto.WerknemerID;
            WerknemerNummer = dto.WerknemerNummer;
            TelefoonNummer = dto.TelefoonNummer;
            Naam = dto.Naam;
            _iWerknemer = iWerknemer;
        }

        public Werknemer(int werknemerID, string naam, int nummerPasje, int telefoonNummer, IWerknemer iWerknemer)
        {            
            WerknemerID = werknemerID;
            Naam = naam;
            WerknemerNummer = nummerPasje;
            TelefoonNummer = telefoonNummer;
            _iWerknemer = iWerknemer;
        }

        public Werknemer(string naam, int nummerPasje, int telefoonNummer, IWerknemer iWerknemer)
        {            
            Naam = naam;
            WerknemerNummer = nummerPasje;
            TelefoonNummer = telefoonNummer;
            _iWerknemer = iWerknemer;
        }

        public int AddWerknemer(int userID)
        {   
            return _iWerknemer.AddNewWerknemer(WerknemerToDTO(), userID);
        }

        public int UpdateWerknemer(int oldWerknemerID)
        {
            return _iWerknemer.UpdateWerknemer(WerknemerToDTO(), oldWerknemerID);
        }

        public int DeleteWerknemer()
        {
           return _iWerknemer.DeleteWerknemer(WerknemerToDTO());
        }

        public int LinkWerknemerToUser(int userID)
        {
            return _iWerknemer.LinkWerknemerToUser(WerknemerToDTO(), userID);
        }

        public WerknemerDTO WerknemerToDTO()
        {
            WerknemerDTO werknemerDTO = new WerknemerDTO();
            werknemerDTO.WerknemerID = WerknemerID;
            werknemerDTO.Naam = Naam;
            werknemerDTO.WerknemerNummer = WerknemerNummer;
            werknemerDTO.TelefoonNummer = TelefoonNummer;
            return werknemerDTO;
        }
    }
}
