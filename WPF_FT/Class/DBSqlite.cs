using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_FT.Class
{
    static class DBSqlite 
    {
        static string startPath = Environment.CurrentDirectory+@"\Data";
        static string sqlitePath = System.IO.Path.Combine(startPath, "a.sqlite");
        static string connectionString = "Data Source=" + sqlitePath + ";";

        /// <summary>
        /// SQLite 조회 쿼리 DataTable 반환
        /// </summary>
        /// <param name="query">조회 쿼리</param>
        /// <returns></returns>
        public static DataTable GetSelectData(string query)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    SQLiteConnection conn = new SQLiteConnection("Data Source=" + sqlitePath + ";");

                    var con = new SqlConnection();

                    SQLiteDataAdapter adpt = new SQLiteDataAdapter(query, conn);
                    adpt.Fill(dt);
                }
                catch (Exception)
                {
                    
                }

                return dt;
            }
        }

        /// <summary>
        /// SQLite 조회 쿼리 단일값 반환
        /// </summary>
        /// <param name="query">조회 쿼리</param>
        /// <returns></returns>
        public static string GetScalar(string query)
        {
            string result = "";

            try
            {
                SQLiteConnection conn = new SQLiteConnection("Data Source=" + sqlitePath + ";");

                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand(query, conn);

                result = cmd.ExecuteScalar().ToString();

                conn.Close();
            }
            catch (Exception)
            {

            }

            return result;
        }


        /// <summary>
        /// 쿼리 실행
        /// </summary>
        /// <param name="query"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool QueryExecute(string query, out string errMsg)
        {
            errMsg = string.Empty;

            try
            {
                SQLiteConnection conn = new SQLiteConnection("Data Source=" + sqlitePath + ";");

                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand(query, conn);

                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception e)
            {
                errMsg = e.ToString();

                return false;
            }

            return true;
        }
    }
}
