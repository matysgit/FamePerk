using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.DAL
{
    public class AccountService : ServiceBase
    {
        public ClientAccountmodal GetClientDetailByEmail(string email)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    string query = @"SELECT ANU.Id
                                     FROM AspNetUsers ANU
                                     INNER JOIN AspNetUserRoles ANUR ON ANUR.UserId = ANU.Id
                                     WHERE ANU.Email = @Email
                                           AND ANUR.RoleId = 2";
                    ClientAccountmodal objData = _dbDapperContext.Query<ClientAccountmodal>(query, new {
                        Email = email
                    }).FirstOrDefault();

                    return objData;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
