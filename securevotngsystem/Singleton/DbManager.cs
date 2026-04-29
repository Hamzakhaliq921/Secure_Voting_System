using System;
using System.Data.SqlClient;

namespace securevotngsystem
{
    public sealed class DbManager
    {
        private static DbManager _instance;
        private static readonly object _lock = new object();
        public SqlConnection Connection { get; }

        private DbManager()
        {
            Connection = new SqlConnection("Data Source=DESKTOP-D6FIOOH\\SQLEXPRESS01;Initial Catalog=securevotingsystem;Integrated Security=True;");
        }

        public static DbManager Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ?? (_instance = new DbManager());
                }
            }
        }
    }
}
