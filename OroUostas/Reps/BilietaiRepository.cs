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
    public class BilietaiRepository
    {
        public List<BilietasViewModel> getBilietai()
        {
            List<BilietasViewModel> bilietaiViewModels = new List<BilietasViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.bilieto_id, m.kaina, mm.asmens_kodas, mm.vardas, mm.pavarde
                                FROM " + @"bilietai m
                                LEFT JOIN " + @"klientai mm ON mm.kliento_id=m.fk_klientaikliento_id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                var sav = Convert.ToString(item["asmens_kodas"]) + " " + Convert.ToString(item["vardas"]) + " " +
                          Convert.ToString(item["pavarde"]);
                bilietaiViewModels.Add(new BilietasViewModel
                {
                    id = Convert.ToInt32(item["bilieto_id"]),
                    kaina = Convert.ToInt32(item["kaina"]),
                    savininkas  = sav
                });
            }

            return bilietaiViewModels;
        }

        public BilietasEditViewModel getBilietas(int id)
        {
            BilietasEditViewModel bilietas = new BilietasEditViewModel();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.* 
                                FROM "+@"bilietai m WHERE m.bilieto_id=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                bilietas.id = Convert.ToInt32(item["bilieto_id"]);
                bilietas.kaina = Convert.ToInt32(item["kaina"]);
                bilietas.fk_klientas = Convert.ToInt32(item["fk_klientaikliento_id"]);
            }

            return bilietas;
        }

        public bool addBilietas(BilietasEditViewModel bilietas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO "+"bilietai(bilieto_id,kaina,fk_klientaikliento_id)VALUES(?id,?kaina,?raktas)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = bilietas.id;
            mySqlCommand.Parameters.Add("?kaina", MySqlDbType.Int32).Value = bilietas.kaina;
            mySqlCommand.Parameters.Add("?raktas", MySqlDbType.VarChar).Value = bilietas.fk_klientas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool updateBilietas(BilietasEditViewModel bilietas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE "+"bilietai a SET a.kaina=?kaina, a.fk_klientaikliento_id=?klientas WHERE a.bilieto_id=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = bilietas.id;
            mySqlCommand.Parameters.Add("?kaina", MySqlDbType.Int32).Value = bilietas.kaina;
            mySqlCommand.Parameters.Add("?klientas", MySqlDbType.Int32).Value = bilietas.fk_klientas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public void deleteBilietas(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM "+"bilietai where bilieto_id=?id";
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
                string sqlquery = @"SELECT MAX(bilieto_id) FROM bilietai";
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