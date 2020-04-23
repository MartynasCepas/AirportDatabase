using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using OroUostas.Models;
using MySql.Data.MySqlClient;

namespace OroUostas.Reps
{
    public class KlientaiRepository
    {
        public List<Klientas> getKlientai()
        {
            var klientai = new List<Klientas>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            var mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from " + "klientai";
            var mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            var mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                klientai.Add(new Klientas
                {
                    id = Convert.ToInt32(item["kliento_id"]),
                    asmens_kodas = Convert.ToInt64(item["asmens_kodas"]),
                    vardas = Convert.ToString(item["vardas"]),
                    pavarde = Convert.ToString(item["pavarde"]),
                    amzius = Convert.ToInt32(item["amzius"]),
                    telefono_numeris = Convert.ToString(item["telefono_numeris"]),
                    elektroninis_pastas = Convert.ToString(item["elektroninis_pastas"])
                });
            }

            return klientai;
        }
    }
}