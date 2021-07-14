using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FP.DAL.Classes
{
    public class WalletDetails : ServiceBase
    {
        public int SaveWalletDetails(WalletAmountModal objData, string userId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;

                    string query = "";
                    var query1 = @"SELECT  UserId, RoleId FROM  AspNetUserRoles Where UserId=@UserId";
                    var outputData = _dbDapperContext.Query(query1, new
                    {

                        UserId = userId
                    }).FirstOrDefault();

                    if (objData.ID == 0)
                    {

                        //query = "Select Count(ID) from WalletAmount where Amount = @Amount";

                        //output = _dbDapperContext.Query<int>(query, new
                        //{
                        //    objData.Amount
                        //}).FirstOrDefault();

                        //if (output < 1)
                        //{
                        if (outputData.RoleId == "3")
                        {
                            // query = "Insert into WalletAmount(Amount,  UploadDate, UploadedBy) values(@Amount, GetUtcDate(), @UploadedBy)";
                            query = "Insert into WalletAmount(Amount,  UploadDate, UploadedBy, TransactionID, Status) values(@Amount, GetUtcDate(), @UploadedBy, @TransactionID, @Status)";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Amount,
                                objData.UploadDate,
                                UploadedBy = userId,
                                objData.TransactionID,
                                objData.Status
                            });
                        }
                        else
                        {
                            //query = "Insert into ClientWalletAmount(Amount,  UploadDate, UploadedBy) values(@Amount, GetUtcDate(), @UploadedBy)";
                            query = "Insert into ClientWalletAmount(Amount,  UploadDate, UploadedBy, TransactionID, Status) values(@Amount, GetUtcDate(), @UploadedBy, @TransactionID, @Status)";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Amount,
                                objData.UploadDate,
                                UploadedBy = userId,
                                objData.TransactionID,
                                objData.Status
                            });
                        }
                        //}
                        //else
                        //{
                        //    output = 0;
                        //}
                    }
                    else
                    {
                        //query = "Select Count(ID) from WalletAmount where Amount = @Amount and ID <> @ID";

                        //output = _dbDapperContext.Query<int>(query, new
                        //{
                        //    objData.Amount,
                        //    objData.UploadDate,
                        //    objData.UploadedBy,
                        //    objData.ID
                        //}).FirstOrDefault();

                        //if (output < 1)
                        //{
                        if (outputData.RoleId == "3")
                        {
                            query = "Update WalletAmount Set Amount = @Amount, UploadDate = GetUtcDate() where ID = @ID";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Amount,
                                objData.UploadDate,
                                UploadedBy = userId,
                                objData.ID
                            });
                        }
                        else
                        {
                            query = "Update ClientWalletAmount Set Amount = @Amount, UploadDate = GetUtcDate() where ID = @ID";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Amount,
                                objData.UploadDate,
                                UploadedBy = userId,
                                objData.ID
                            });
                        }
                        //}
                        //else
                        //{
                        //    output = 0;
                        //}
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

        public List<WalletAmountModal> GetWalletAmountList(string userId)
        {
            try
            {
                int roleId = 0;
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {

                 var   query1 = @"SELECT  UserId, RoleId FROM  AspNetUserRoles Where UserId=@UserId";
                 var   output = _dbDapperContext.Query(query1, new
                    {
                        
                        UserId = userId
                    }).FirstOrDefault();

                    
                    //foreach (var data in output)
                    //{
                    //    roleId =Convert.ToInt32( data.RoleId);
                    //}
                    if (output.RoleId == "3")
                    {
                        string query = "Select ID,  Amount, FORMAT(UploadDate, 'dd/MM/yyyy ') as UploadDate, UploadedBy from WalletAmount where UploadedBy=@UploadedBy order by ID desc ";
                        List<WalletAmountModal> _objData = _dbDapperContext.Query<WalletAmountModal>(query, new
                        {
                            UploadedBy = userId
                        }).ToList();
                        return _objData;
                    }
                    else
                    {
                        string query = "Select ID,  Amount, FORMAT(UploadDate, 'dd/MM/yyyy ') as UploadDate, UploadedBy from ClientWalletAmount where UploadedBy=@UploadedBy order by ID desc ";
                        List<WalletAmountModal> _objData = _dbDapperContext.Query<WalletAmountModal>(query, new
                        {
                            UploadedBy = userId
                        }).ToList();
                        return _objData;
                    }
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public WalletAmountModal GetWalletAmountById(int Id, string userId)
        {
            try
            {
                int roleId = 0;
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    var query1 = @"SELECT  UserId, RoleId FROM  AspNetUserRoles Where UserId=@UserId";
                    var output = _dbDapperContext.Query(query1, new
                    {

                        UserId = userId
                    }).FirstOrDefault();


                    //foreach (var data in output)
                    //{
                    //    roleId = data.RoleId;
                    //}
                    if (output.RoleId == "3")
                    {
                        string query = "Select ID, Amount from WalletAmount where ID = @ID";
                        WalletAmountModal _objData = _dbDapperContext.Query<WalletAmountModal>(query, new
                        {
                            ID = Id
                        }).FirstOrDefault();
                        return _objData;
                    }
                    else
                    {
                        string query = "Select ID, Amount from ClientWalletAmount where ID = @ID";
                        WalletAmountModal _objData = _dbDapperContext.Query<WalletAmountModal>(query, new
                        {
                            ID = Id
                        }).FirstOrDefault();
                        return _objData;
                    }
                }
            }
            catch (Exception)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public int RemoveWalletAmount(int Id, string userId)
        {
            try
            {
                int roleId = 0;
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    var query1 = @"SELECT  UserId, RoleId FROM  AspNetUserRoles Where UserId=@UserId";
                    var outputData = _dbDapperContext.Query(query1, new
                    {

                        UserId = userId
                    }).FirstOrDefault();


                    //foreach (var data in outputData)
                    //{
                    //    roleId = data.RoleId;
                    //}
                    if (outputData.RoleId == "3")
                    {
                        int output = _dbDapperContext.Execute("Delete from WalletAmount where ID = @ID", new
                        {
                            ID = Id
                        });
                        return output;
                    }
                    else
                    {
                        int output = _dbDapperContext.Execute("Delete from ClientWalletAmount where ID = @ID", new
                        {
                            ID = Id
                        });
                        return output;
                    }
                    
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
