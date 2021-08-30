using System.Data.SqlClient;
using System.Configuration;

namespace FP.DAL
{
    public class ServiceBase
    {
        protected string _dbConnectionString = null;

        public ServiceBase()
        {
            this._dbConnectionString = GetDefaultConnectionString();
        }

        protected SqlConnection GetDefaultConnection()
        {
            return new SqlConnection(_dbConnectionString);
        }

        public string GetDefaultConnectionString()
        {
            string dataSource = "DESKTOP-2R2BMUA\\SQLEXPRESS";
            string dbName = "FPNew21";
            string username = "sa";
            string password = "admin";

            var dbConnectionString = @"data source= " + dataSource + "; " +
                "initial catalog=" + dbName + "; persist security info=True; " +
                "user id=" + username + "; " +
                "password=" + password;

            return dbConnectionString;
        }
    }
}
