using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FP.DAL
{
    public class CampaignType : ServiceBase
    {


        public int SaveCampaignType(CampaignTypeModal objData)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;

                    string query = "";

                    if (objData.CampaignTypeId == 0)
                    {
                        query = "Select Count(CampaignTypeId) from CampaignType where Title = @Title and IsDeleted = 0";

                        output = _dbDapperContext.Query<int>(query, new
                        {
                            objData.Title
                        }).FirstOrDefault();

                        if (output < 1)
                        {
                            query = "Insert into CampaignType(Title, CreatedBy, CreatedDate) values(@Title, @CreatedBy, GetUtcDate())";
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
                        query = "Select Count(CampaignTypeId) from CampaignType where Title = @Title and IsDeleted = 0 and CampaignTypeId <> @CampaignTypeId";

                        output = _dbDapperContext.Query<int>(query, new
                        {
                            objData.Title,
                            objData.CampaignTypeId
                        }).FirstOrDefault();

                        if (output < 1)
                        {
                            query = "Update CampaignType Set Title = @Title, ModifiedBy = @ModifiedBy, ModifiedDate = GetUtcDate() where CampaignTypeId = @CampaignTypeId";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Title,
                                ModifiedBy = objData.ManagedBy,
                                objData.CampaignTypeId
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

        public List<CampaignTypeModal> GetCampaignTypeList()
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    string query = "Select CampaignTypeId, Title, FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from CampaignType order by CampaignTypeId desc";
                    List<CampaignTypeModal> _objData = _dbDapperContext.Query<CampaignTypeModal>(query).ToList();

                    return _objData;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public CampaignTypeModal GetCampaignTypeById(int CampaignTypeId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {

                    string query = "Select CampaignTypeId, Title from CampaignType where CampaignTypeId = @CampaignTypeId";
                    CampaignTypeModal _objData = _dbDapperContext.Query<CampaignTypeModal>(query, new
                    {
                        CampaignTypeId = CampaignTypeId
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

        public int RemoveCampaignType(int CampaignTypeId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = _dbDapperContext.Execute("Delete from CampaignType where CampaignTypeId = @CampaignTypeId", new
                    {
                        CampaignTypeId = CampaignTypeId
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
