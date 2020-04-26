using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using OroUostas.Models;

namespace OroUostas.Reps
{
    public class KryptysRepository
    {
        public List<Kryptis> getKryptys()
        {
            var kryptys = new List<Kryptis>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            var mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from " + "kryptys";
            var mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            var mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                kryptys.Add(new Kryptis
                {
                    id = Convert.ToInt32(item["kryptis_id"]),
                    iOroUosta = Convert.ToString(item["i_oro_uosta"]),
                    isOroUosto = Convert.ToString(item["is_oro_uosto"]),
                    trukme = Convert.ToString(item["trukme"])
                });
            }

            return kryptys;
        }
    }
}