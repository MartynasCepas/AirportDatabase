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
    public class LektuvaiRepository
    {
        public List<Lektuvas> getLektuvai()
        {
            var lektuvai = new List<Lektuvas>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            var mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from " + "lektuvai";
            var mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            var mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                lektuvai.Add(new Lektuvas()
                {
                    id = Convert.ToInt32(item["kebulo_nr"]),
                    modelis = Convert.ToString(item["modelis"]),
                    kategorija = Convert.ToString(item["kategorija"]),
                    pagaminimoMetai = Convert.ToInt32(item["pagaminimo_metai"]),
                    svoris = Convert.ToInt32(item["svoris"]),
                    vietuSkaicius = Convert.ToInt32(item["vietu_skaicius"]),
                });
            }

            return lektuvai;
        }
    }
}