using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Database
{
    public class Database
    {

        public string DatabaseName_;
        public string Host_;
        public int Port_;
        public string Username_;
        public string Password_;


        public Database(string name, string host, string username, string password, int port = 5432)
        {
            DatabaseName_ = name;
            Host_ = host;
            Port_ = port;
            Username_ = username;
            Password_ = password;
        }

        public string ConnectionString => $"Host={Host_};Username={Username_};Password={Password_};Database={DatabaseName_}";

    }
}
