using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FP.DAL
{
    public class Budget : ServiceBase
    {


        public int SaveBudget(BudgetModal objData)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;

                    string query = "";

                    if (objData.BudgetId == 0)
                    {
                        query = "Select Count(BudgetId) from Budget where Title = @Title and IsDeleted = 0";

                        output = _dbDapperContext.Query<int>(query, new
                        {
                            objData.Title
                        }).FirstOrDefault();

                        if (output < 1)
                        {
                            query = "Insert into Budget(Title, CreatedBy, CreatedDate) values(@Title, @CreatedBy, GetUtcDate())";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Title,
                                CreatedBy = objData.ManagedBy
                            });
                        }
                        else
                        {
                            output = 0;
                        }
                    }
                    else
                    {
                        query = "Select Count(BudgetId) from Budget where Title = @Title and IsDeleted = 0 and BudgetId <> @BudgetId";

                        output = _dbDapperContext.Query<int>(query, new
                        {
                            objData.Title,
                            objData.BudgetId
                        }).FirstOrDefault();

                        if (output < 1)
                        {
                            query = "Update Budget Set Title = @Title, ModifiedBy = @ModifiedBy, ModifiedDate = GetUtcDate() where BudgetId = @BudgetId";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Title,
                                ModifiedBy = objData.ManagedBy,
                                objData.BudgetId
                            });
                        }
                        else
                        {
                            output = 0;
                        }
                    }
                    return output;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return -1;
            }
        }

        public List<BudgetModal> GetBudgetList()
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    string query = "Select BudgetId, Title, FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from Budget order by BudgetId desc";
                    List<BudgetModal> _objData = _dbDapperContext.Query<BudgetModal>(query).ToList();

                    return _objData;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public BudgetModal GetBudgetById(int BudgetId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {

                    string query = "Select BudgetId, Title from Budget where BudgetId = @BudgetId";
                    BudgetModal _objData = _dbDapperContext.Query<BudgetModal>(query, new
                    {
                        BudgetId = BudgetId
                    }).FirstOrDefault();

                    return _objData;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public int RemoveBudget(int BudgetId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = _dbDapperContext.Execute("Delete from Budget where BudgetId = @BudgetId", new
                    {
                        BudgetId = BudgetId
                    });
                    return output;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return -1;
            }
        }

    }
}
