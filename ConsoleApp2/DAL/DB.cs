﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL.DAL
{
    public class DB
    {
        protected MySqlConnection connection;
        private readonly string connString = "Server = studmysql01.fhict.local; Uid=dbi485050;Database=dbi485050;Pwd=Vredeoord123;";

        public DB()
        {
            initialize();
        }

        private void initialize()
        {
            connection = new MySqlConnection(connString);
        }

        protected bool openConnection()
        {
            if (connection.State == ConnectionState.Open)
                return true;

            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        break;

                    case 1045:
                        break;
                }
                return false;
            }
        }

        protected bool closeConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }
    }
}
