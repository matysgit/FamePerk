using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FP.DAL
{
  public  class CountryList : ServiceBase
    {
        
        public List<CountryModal> GetCountry()
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    string query = "SELECT CountryId, Name FROM Country";
                    List<CountryModal> _objData = _dbDapperContext.Query<CountryModal>(query).ToList();

                    return _objData;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public List<CurrencyTypeModal> GetCurrency()
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    string query = "SELECT CurrencyId, CurrencyName FROM CurrencyType";
                    List<CurrencyTypeModal> _objData = _dbDapperContext.Query<CurrencyTypeModal>(query).ToList();

                    return _objData;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
