using ProgramCore.ObjectModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SQLiteComponent
{
    public class DBManager
    {
        private SQLite sql;
        #region Private Func

        private bool MakeCactusListDB()
        {
            return sql.ExecuteSQL("CREATE TABLE CACTUSLIST (" +
                "cactus_uid INT," +
                "cactus_name VARCHAR(50)," +
                "cactus_price INT);");
        }

        private bool MakeCacutList()
        {
            return sql.ExecuteSQL("INSERT INTO CACTUSLIST(cactus_uid, cactus_name, cactus_price) VALUES " +
            "(0,'선인장1',15000)," +
            "(1,'선인장2',30000)," +
            "(2,'선인장3',45000);");
        }
        #endregion


        public void CloseDB()
        {
            sql.CloseDataBase();
        }
        public DBManager()
        {
            sql = new SQLite(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\JejuReceiptSetting\\", "data");
            try
            {
                if (!sql.OpenDataBase())
                {
                    if (sql.CreateDataBase())
                    {
                        if (!sql.OpenDataBase())
                            throw new Exception("DB에 연결할 수 없습니다.");

                        if (!MakeCactusListDB())
                            throw new Exception("DB를 생성할 수 없습니다");
                        MakeCacutList();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public int GetMaxUid()
        {
            try
            {
                sql.ExecuteSQL("SELECT MAX(cactus_uid) FROM CACTUSLIST");
                var temp = sql.GetData();
                if (temp.Count > 0)
                    return Convert.ToInt32(temp[0]["MAX(cactus_uid)"]) + 1;
                return 0;
                
            }
            catch(System.InvalidCastException ex)
            {
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("최대 uid를 불러올 수 없습니다.");
            }
        }

        public List<CactusListForm> LoadCactusList()
        {
            List<CactusListForm> list = new List<CactusListForm>();

            sql.ExecuteSQL("SELECT * FROM CACTUSLIST ORDER BY cactus_uid;");
            List<Dictionary<string, object>> temp = sql.GetData();

            for (int i = 0; i < temp.Count; i++)
            {
                list.Add(new CactusListForm()
                {
                    Index = Convert.ToInt32(temp[i]["cactus_uid"]),
                    Title = temp[i]["cactus_name"].ToString(),
                    Price = Convert.ToInt32(temp[i]["cactus_price"])
                });
            }

            return list;
        }


        public bool InsertCactusData(CactusListForm cactus)
        {
            try
            {
                if (cactus.Index < 0)
                    return false;

                sql.ExecuteSQL("SELECT * FROM CACTUSLIST where cactus_uid = " + cactus.Index);
                var temp = sql.GetData();
                if (temp.Count == 0)
                {
                    sql.ExecuteSQL("UPDATE CACTUSLIST set cactus_uid = cactus_uid + 1 WHERE cactus_uid >= " + cactus.Index);
                    return sql.ExecuteSQL("INSERT INTO CACTUSLIST(cactus_uid, cactus_name, cactus_price) VALUES " +
                                          "(" + cactus.Index + ",'" + cactus.Title + "'," + cactus.Price + ");");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("선인장을 추가할 수 없습니다.");
            }
        }

        public bool UpdateCactusData(CactusListForm cactus)
        {
            try
            {
                sql.ExecuteSQL("SELECT * FROM CACTUSLIST where cactus_uid = " + cactus.Index);
                var temp = sql.GetData();
                if (temp.Count > 0)
                {
                    return sql.ExecuteSQL("UPDATE CACTUSLIST SET cactus_name = '" + cactus.Title + "'," +
                        "cactus_price = " + cactus.Price + " WHERE cactus_uid = " + cactus.Index + ";");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("선인장을 추가할 수 없습니다.");
            }
        }

        public bool DeleteCactusData(int idx)
        {
            try
            {
                sql.ExecuteSQL("SELECT * FROM CACTUSLIST where cactus_uid = " + idx);
                var temp = sql.GetData();
                if (temp.Count > 0)
                {
                    if (sql.ExecuteSQL("DELETE FROM CACTUSLIST WHERE cactus_uid = " + idx))
                    {
                        if (sql.ExecuteSQL("UPDATE CACTUSLIST set cactus_uid = cactus_uid - 1 WHERE cactus_uid > " + idx))
                            return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("선인장을 삭제할 수 없습니다.");
            }
        }
    }
}
