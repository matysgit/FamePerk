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
            var dbConnectionString = @"data source=LAPTOP-3L09R40I\SQLEXPRESS; initial catalog=FPNew21; persist security info=True; user id=sa; password=Test123";
            //var dbConnectionString = @"data source=KRISHNA-PC; initial catalog=FPNew; persist security info=True; user id=sa; password=Reset123";

            return dbConnectionString;
        }
    }
}
