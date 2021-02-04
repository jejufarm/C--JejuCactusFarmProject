using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteComponent
{
    public interface ISQLite
    {
        bool CreateDataBase();

        bool OpenDataBase();

        bool CloseDataBase();

        bool ExecuteSQL(string sql);

        bool ExecuteSQL(string[] sql);
    }
}
