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
    public class SkrydzioBilietasRepository
    {
        public List<SkrydzioBilietasViewModel> getSkrydzioBilietai()
        {
            List<SkrydzioBilietasViewModel> skrydzioBilietasViewModels = new List<SkrydzioBilietasViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from " + "skrydzio_bilietai";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                skrydzioBilietasViewModels.Add(new SkrydzioBilietasViewModel
                {
                    id = Convert.ToInt32(item["skrydzio_bilietai_id"]),
                    fk_skrydis = Convert.ToInt32(item["fk_skrydziaiskrydzio_id"]),
                    fk_bilietas = Convert.ToInt32(item["fk_bilietaibilieto_id"])
                });
            }

            return skrydzioBilietasViewModels;
        }

        public SkrydzioBilietasEditViewModel getBilietas(int id)
        {
            SkrydzioBilietasEditViewModel bilietas = new SkrydzioBilietasEditViewModel();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.* 
                                FROM "+@"skrydzio_bilietai m WHERE m.skrydzio_bilietai_id=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                bilietas.id = Convert.ToInt32(item["skrydzio_bilietai_id"]);
                bilietas.fk_bilietas = Convert.ToInt32(item["fk_bilietaibilieto_id"]);
                bilietas.fk_skrydis = Convert.ToInt32(item["fk_skrydziaiskrydzio_id"]);
            }

            return bilietas;
        }

        public bool addBilietas(SkrydzioBilietasEditViewModel bilietas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO "+"skrydzio_bilietai(skrydzio_bilietai_id,fk_bilietaibilieto_id,fk_skrydziaiskrydzio_id)VALUES(?id,?bilietas,?skrydis)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = bilietas.id;
            mySqlCommand.Parameters.Add("?bilietas", MySqlDbType.Int32).Value = bilietas.fk_bilietas;
            mySqlCommand.Parameters.Add("?skrydis", MySqlDbType.VarChar).Value = bilietas.fk_skrydis;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool updateBilietas(SkrydzioBilietasEditViewModel bilietas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE "+"skrydzio_bilietai a SET a.fk_bilietaibilieto_id=?bilietas, a.fk_skrydziaiskrydzio_id=?skrydis WHERE a.skrydzio_bilietai_id=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = bilietas.id;
            mySqlCommand.Parameters.Add("?bilietas", MySqlDbType.Int32).Value = bilietas.fk_bilietas;
            mySqlCommand.Parameters.Add("?skrydis", MySqlDbType.Int32).Value = bilietas.fk_skrydis;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public void deleteBilietas(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM "+"skrydzio_bilietai where skrydzio_bilietai_id=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        public int getNewId()
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"SELECT MAX(skrydzio_bilietai_id) FROM skrydzio_bilietai";
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