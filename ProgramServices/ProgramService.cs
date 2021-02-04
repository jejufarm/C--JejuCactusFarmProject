using SQLiteComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramServices
{
    public static class ProgramService
    {
        public static DBManager db;

        public static bool RunDB()
        {
            try
            {
                db = new DBManager();
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("DB를 불러올 수 없습니다.");
            }
        }
        public static void CloseDB()
        {
            db.CloseDB();
        }
    }
}
