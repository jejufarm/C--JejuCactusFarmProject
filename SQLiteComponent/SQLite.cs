using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using SQLiteComponent.User_Defined_Exception;

namespace SQLiteComponent
{
    public class SQLite : ISQLite
    {
        /******************************/
        private string file_path = null;
        private string file_name = null;
        /******************************/
        /*******************************/
        /********************************/
        /*********************************/
        /**********************************/
        /***********************************/
        private SQLiteConnection conn = null;
        private SQLiteCommand command = null;
        /***********************************/
        private bool connected = false;

        #region Constructor and Destructor
        public SQLite()
        {
            file_path = Environment.CurrentDirectory + "\\data";
            file_name = "data.sqlite";
        }
        public SQLite(string file_path, string file_name)
        {
            this.file_path = file_path + "\\";
            this.file_name = file_name + ".sqlite";
        }
        ~SQLite()
        {
            CloseDataBase();
        }
        #endregion

        #region SQL init
        public bool CreateDataBase()
        {
            try
            {
                SQLiteConnection.CreateFile(file_path + file_name);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool OpenDataBase()
        {
            try
            {
                if (File.Exists(file_path + file_name))
                {
                    conn = new SQLiteConnection("Data Source=" + file_path + file_name + ";Version=3;");
                    conn.Open();
                    connected = true;
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                connected = false;
                return false;
            }
        }

        public bool CloseDataBase()
        {
            try
            {
                if (conn != null)
                {
                    conn.Close();
                    connected = false;
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region SQL execute
        public bool ExecuteSQL(string sql)
        {
            if (connected != true)
                throw new NotConnectSQLiteException("SQLite연결이 되지 않았습니다.");
            try
            {
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool ExecuteSQL(string[] sql)
        {
            if (connected == true)
                throw new NotConnectSQLiteException("SQLite연결이 되지 않았습니다.");
            try
            {
                foreach (var str in sql)
                {
                    SQLiteCommand command = new SQLiteCommand(str, this.conn);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<Dictionary<string, object>> GetData()
        {
            try
            {
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                if (command != null)
                {
                    SQLiteDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        Dictionary<string, object> dict = new Dictionary<string, object>();
                        for (int idx = 0; idx < rdr.FieldCount; idx++)
                            dict.Add(rdr.GetName(idx), rdr.GetValue(idx));

                        list.Add(new Dictionary<string, object>(dict));

                    }
                    rdr.Close();
                    return list;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        #endregion
    }
}
