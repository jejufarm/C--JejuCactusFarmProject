using System;

namespace SQLiteComponent.User_Defined_Exception
{
    public class NotConnectSQLiteException: Exception
    {
        public NotConnectSQLiteException() { }

        public NotConnectSQLiteException(string message)
            : base(message)
        {

        }
        public NotConnectSQLiteException(string message, Exception inner)
            :base(message,inner)
        {

        }
    }
}
