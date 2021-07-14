using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FP.DAL.Classes
{
    public class BankDetail : ServiceBase
    {
        public int SaveBankDetails(BankDetailModal objData, string userId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;

                    string query = "";
                    
                    if (objData.BankId == 0)
                    {
                        query = "Select Count(AccountNumber) from BankDetail where UserId = @UserId";

                        output = _dbDapperContext.Query<int>(query, new
                        {
                           UserId=userId
                        }).FirstOrDefault();

                        if (output > 0)
                        {
                            return output = 0;
                        }
                        else
                        {
                            query = "Insert into BankDetail(BankName,  BankAddress, BankCode, AccountNumber, AccountHolderName, UserId) values(@BankName, @BankAddress, @BankCode, @AccountNumber, @AccountHolderName, @UserId)";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.BankName,
                                objData.BankAddress,
                                objData.BankCode,
                                objData.AccountNumber,
                                objData.AccountHolderName,
                                UserId = userId
                            });
                        }
                        
                    }
                    else
                    {
                        query = "Select Count(AccountNumber) from BankDetail where UserId = @UserId and BankId<> @BankId";

                        output = _dbDapperContext.Query<int>(query, new
                        {
                            UserId = userId,
                            objData.BankId
                        }).FirstOrDefault();

                        if (output > 0)
                        {
                            return output = 0;
                        }
                        else
                        {
                            query = "Update BankDetail Set BankName = @BankName, BankAddress = @BankAddress, BankCode=@BankCode, AccountNumber=@Accountnumber, AccountHolderName=@AccountHolderName where BankId = @BankId";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.BankName,
                                objData.BankAddress,
                                objData.BankCode,
                                objData.AccountNumber,
                                objData.AccountHolderName,
                                objData.BankId
                            });
                        }
                        
                    }
                    return output;
                }
            }
            catch (Exception ex)
            {
                //string str = e.ToString();
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return -1;
            }
        }

        public List<BankDetailModal> GetBankList(string userId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {

                        string query = "SELECT BankId, AccountNumber, AccountHolderName, BankName, BankAddress, BankCode, UserId FROM BankDetail where UserId=@UserId order by BankId desc ";
                        List<BankDetailModal> _objData = _dbDapperContext.Query<BankDetailModal>(query, new
                        {
                            UserId = userId
                        }).ToList();
                        return _objData;
                   
                   
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public BankDetailModal GetBankDetailById(int Id, string userId)
        {
            try
            {
                
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                        string query = "SELECT BankId, AccountNumber, AccountHolderName, BankName, BankAddress, BankCode, UserId FROM BankDetail where BankId = @BankId";
                        BankDetailModal _objData = _dbDapperContext.Query<BankDetailModal>(query, new
                        {
                            BankId = Id
                        }).FirstOrDefault();
                        return _objData;
                   
                }
            }
            catch (Exception)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public int RemoveBank(int Id, string userId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                   
                        int output = _dbDapperContext.Execute("Delete from BankDetail where BankId = @BankId", new
                        {
                            BankId = Id
                        });
                        return output;

                }
            }
            catch (Exception)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return -1;
            }
        }
    }
}
