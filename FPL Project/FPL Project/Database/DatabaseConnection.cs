using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Database
{
    public class DatabaseConnection
    {

        private Database Database_;
        private NpgsqlConnection Conn_;
        private NpgsqlTransaction? Trans_;

        public DatabaseConnection(Database database)
        {
            Database_ = database;
            Conn_ = new NpgsqlConnection(Database_.ConnectionString);
        }


        public async Task BeginTransaction()
        {
            if (Trans_ is not null) throw new Exception("Transaction already created");
            Trans_ = await Conn_.BeginTransactionAsync();
        }

        public async Task CreateTable(string TableName, List<string> ColNames)
        {

        }


        public async Task CancelTransaction()
        {
            if (Trans_ is null) throw new Exception("No transaction in progress");
            await Trans_.RollbackAsync();
            Trans_ = null;
        }

        public async Task CommitTransaction()
        {
            if (Trans_ is null) throw new Exception("No transaction in progress");
            await Trans_.CommitAsync();
            Trans_ = null;
        }
    }
}
