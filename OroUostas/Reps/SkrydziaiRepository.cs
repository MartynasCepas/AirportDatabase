using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using OroUostas.ViewModels;

namespace OroUostas.Reps
{
    public class SkrydziaiRepository
    {
        public List<SkrydisViewModel> getSkrydziai()
        {
            List<SkrydisViewModel> skrydisViewModels = new List<SkrydisViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.skrydzio_id, m.skrydzio_data, m.skrydzio_laikas, mm.kebulo_nr, mm.modelis, mmm.is_oro_uosto, mmm.i_oro_uosta
                                FROM " + @"skrydziai m
                                LEFT JOIN " + @"lektuvai mm ON mm.kebulo_nr=m.fk_lektuvaikebulo_nr
                                LEFT JOIN kryptys mmm on mmm.kryptis_id=m.fk_kryptyskryptis_id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                var lektuvas = Convert.ToString(item["kebulo_nr"]) + " " + Convert.ToString(item["modelis"]);
                var kryptis = "Is: " + Convert.ToString(item["is_oro_uosto"]) + " I: " + Convert.ToString(item["i_oro_uosta"]);
                var data = Convert.ToString(item["skrydzio_data"]) + " " + Convert.ToString(item["skrydzio_laikas"]);
                skrydisViewModels.Add(new SkrydisViewModel
                {
                    id = Convert.ToInt32(item["skrydzio_id"]),
                    data = data,
                    kryptis = kryptis,
                    lektuvas = lektuvas
                });
            }

            return skrydisViewModels;
        }

        public SkrydisEditViewModel getSkrydis(int id)
        {
            SkrydisEditViewModel skrydis = new SkrydisEditViewModel();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.* 
                                FROM "+@"skrydziai m WHERE m.skrydzio_id=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            

            foreach (DataRow item in dt.Rows)
            {
                var t = Convert.ToDateTime(item["skrydzio_laikas"]); 
                var time = new TimeSpan(t.Hour,t.Minute,t.Second);
                skrydis.id = Convert.ToInt32(item["skrydzio_id"]);
                skrydis.data = Convert.ToDateTime(item["skrydzio_data"]);
                skrydis.laikas = time;
                skrydis.fk_lektuvas = Convert.ToInt32(item["fk_lektuvaikebulo_nr"]);
                skrydis.fk_kryptis = Convert.ToInt32(item["fk_kryptyskryptis_id"]);
            }

            return skrydis;
        }

        public bool addSkrydis(SkrydisEditViewModel skrydis)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO "+"skrydziai(skrydzio_id,skrydzio_data,skrydzio_laikas,fk_lektuvaikebulo_nr,fk_kryptyskryptis_id)VALUES(?id,?data,?laikas,?lektuvas,?kryptis)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = skrydis.id;
            var data = new DateTime(skrydis.data.Year, skrydis.data.Month, skrydis.data.Day);
            mySqlCommand.Parameters.Add("?data", MySqlDbType.Date).Value = data;
            skrydis.laikas.Add(new TimeSpan(skrydis.data.Hour, skrydis.data.Minute, skrydis.data.Second));
            mySqlCommand.Parameters.Add("?laikas", MySqlDbType.Time).Value = skrydis.laikas;
            mySqlCommand.Parameters.Add("?lektuvas", MySqlDbType.Int32).Value = skrydis.fk_lektuvas;
            mySqlCommand.Parameters.Add("?kryptis", MySqlDbType.Int32).Value = skrydis.fk_kryptis;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public int getNewId()
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"SELECT MAX(skrydzio_id) FROM skrydziai";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlConnection.Open();
                int id = (int)mySqlCommand.ExecuteScalar();
                mySqlConnection.Close();
                return id+1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }
    }
}