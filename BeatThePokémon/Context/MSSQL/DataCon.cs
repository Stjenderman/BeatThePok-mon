using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Context.MSSQL
{
    public class DataCon
    {
        const string connectionString = "";
        public SqlConnection conn = new SqlConnection(connectionString);

        public SqlConnection OpenConn()
        {
            try
            {
                conn.Open();
            }
            catch
            {
                throw;
            }

            return conn;
        }

        public bool CloseConn()
        {
            bool isExecute;
            try
            {
                conn.Close();
                isExecute = true;
            }
            catch
            {
                isExecute = false;
                throw;
            }

            return isExecute;
        }

    }
}
