﻿using DAL.DAL;
using Interfaces.DTO;
using Interfaces.Interface;
using Logic.Models;

namespace Logic.Containers
{
    public class WerknemerContainer
    {
        private IWerknemerContainer _iWerknemerContainer;
        private IWerknemer _iWerknemer;

        public WerknemerContainer(IWerknemerContainer iWerknemerContainer)
        {
            _iWerknemerContainer = iWerknemerContainer;
            _iWerknemer = (IWerknemer)iWerknemerContainer;
        }

        public Werknemer GetSingleWerknemer(int ID)
        {
            try
            {
                WerknemerDTO searchedWerknemer = _iWerknemerContainer.GetSingleWerknemer(ID);
                return new Werknemer(searchedWerknemer.WerknemerID, searchedWerknemer.Naam, searchedWerknemer.WerknemerNummer, searchedWerknemer.TelefoonNummer, _iWerknemer);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Werknemer> GetAllWerknemers()
        {
            List<Werknemer> werknemers = new List<Werknemer>();
            try
            {
                foreach (WerknemerDTO searchedWerknemer in _iWerknemerContainer.GetAllWerknemers())
                {
                    Werknemer werknemer = new Werknemer(searchedWerknemer, _iWerknemer);
                    werknemers.Add(werknemer);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return werknemers;
        }

        public List<Werknemer> GetUserWerknemers(int userID)
        {
            List<Werknemer> werknemers = new List<Werknemer>();
            try
            {
                foreach (WerknemerDTO searchedWerknemer in _iWerknemerContainer.GetUserWerknemers(userID))
                {
                    Werknemer werknemer = new Werknemer(searchedWerknemer, _iWerknemer);
                    werknemers.Add(werknemer);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return werknemers;
        }
    }
}
