using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Mvc;
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

        public Klientas getKlientas(long kodas)
        {
            Klientas klientas = new Klientas();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from " + "klientai where asmens_kodas="+kodas;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?asmens_kodas", MySqlDbType.Int64).Value = kodas;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                klientas.id = Convert.ToInt32(item["kliento_id"]);
                klientas.asmens_kodas = Convert.ToInt64(item["asmens_kodas"]);
                klientas.vardas = Convert.ToString(item["vardas"]);
                klientas.pavarde = Convert.ToString(item["pavarde"]);
                klientas.amzius = Convert.ToInt32(item["amzius"]);
                klientas.telefono_numeris = Convert.ToString(item["telefono_numeris"]);
                klientas.elektroninis_pastas = Convert.ToString(item["elektroninis_pastas"]);
            }
            return klientas;
        }

        public Klientas getKlientas(int kodas)
        {
            Klientas klientas = new Klientas();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from " + "klientai where kliento_id="+kodas;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?kliento_id", MySqlDbType.Int32).Value = kodas;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                klientas.id = Convert.ToInt32(item["kliento_id"]);
                klientas.asmens_kodas = Convert.ToInt64(item["asmens_kodas"]);
                klientas.vardas = Convert.ToString(item["vardas"]);
                klientas.pavarde = Convert.ToString(item["pavarde"]);
                klientas.amzius = Convert.ToInt32(item["amzius"]);
                klientas.telefono_numeris = Convert.ToString(item["telefono_numeris"]);
                klientas.elektroninis_pastas = Convert.ToString(item["elektroninis_pastas"]);
            }
            return klientas;
        }

        public bool addKlientas(Klientas klientas)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"INSERT INTO "+"klientai(kliento_id,asmens_kodas,vardas,pavarde,amzius,telefono_numeris,elektroninis_pastas)VALUES(?kliento_id,?asmens_kodas,?vardas,?pavarde,?amzius,?telefono_numeris,?elektroninis_pastas);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?kliento_id", MySqlDbType.Int32).Value = klientas.id;
                mySqlCommand.Parameters.Add("?asmens_kodas", MySqlDbType.Int64).Value = klientas.asmens_kodas;
                mySqlCommand.Parameters.Add("?vardas", MySqlDbType.VarChar).Value = klientas.vardas;
                mySqlCommand.Parameters.Add("?pavarde", MySqlDbType.VarChar).Value = klientas.pavarde;
                mySqlCommand.Parameters.Add("?amzius", MySqlDbType.Int32).Value = klientas.amzius;
                mySqlCommand.Parameters.Add("?telefono_numeris", MySqlDbType.VarChar).Value = klientas.telefono_numeris;
                mySqlCommand.Parameters.Add("?elektroninis_pastas", MySqlDbType.VarChar).Value = klientas.elektroninis_pastas;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int getNewId()
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"SELECT MAX(kliento_id) FROM klientai";
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

        public void deleteKlientas(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM "+"klientai where kliento_id="+id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        public int getKlientasBilietuCount(int id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(bilieto_id) as kiekis from "+"bilietai where fk_klientaikliento_id=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                naudota = Convert.ToInt32(item["kiekis"] == DBNull.Value ? 0 : item["kiekis"]);
            }
            return naudota;
        }

        public bool updateKlientas(Klientas klientas)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"UPDATE "+"klientai a SET a.vardas=?vardas, a.pavarde=?pavarde, a.amzius=?amzius, a.telefono_numeris=?tel, a.elektroninis_pastas=?email, a.asmens_kodas=?asmens_kodas WHERE a.kliento_id=?kliento_id";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?kliento_id", MySqlDbType.Int32).Value = klientas.id;
                mySqlCommand.Parameters.Add("?asmens_kodas", MySqlDbType.Int64).Value = klientas.asmens_kodas;
                mySqlCommand.Parameters.Add("?vardas", MySqlDbType.VarChar).Value = klientas.vardas;
                mySqlCommand.Parameters.Add("?pavarde", MySqlDbType.VarChar).Value = klientas.pavarde;
                mySqlCommand.Parameters.Add("?amzius", MySqlDbType.Int32).Value = klientas.amzius;
                mySqlCommand.Parameters.Add("?tel", MySqlDbType.VarChar).Value = klientas.telefono_numeris;
                mySqlCommand.Parameters.Add("?email", MySqlDbType.VarChar).Value = klientas.elektroninis_pastas;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}