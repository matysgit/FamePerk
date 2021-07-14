using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FP.DAL
{
    public class CampaignDuration : ServiceBase
    {


        public int SaveCampaignDuration(CampaignDurationModal objData)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;

                    string query = "";

                    if (objData.CampaignDurationId == 0)
                    {
                        query = "Select Count(CampaignDurationId) from CampaignDuration where Duration = @Duration and IsDeleted = 0";

                        output = _dbDapperContext.Query<int>(query, new
                        {
                            objData.Duration
                        }).FirstOrDefault();

                        if (output < 1)
                        {
                            query = "Insert into CampaignDuration(Duration, CreatedBy, CreatedDate) values(@Duration, @CreatedBy, GetUtcDate())";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Duration,
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
                        query = "Select Count(CampaignDurationId) from CampaignDuration where Duration = @Duration and IsDeleted = 0 and CampaignDurationId <> @CampaignDurationId";

                        output = _dbDapperContext.Query<int>(query, new
                        {
                            objData.Duration,
                            objData.CampaignDurationId
                        }).FirstOrDefault();

                        if (output < 1)
                        {
                            query = "Update CampaignDuration Set Duration = @Duration, ModifiedBy = @ModifiedBy, ModifiedDate = GetUtcDate() where CampaignDurationId = @CampaignDurationId";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Duration,
                                ModifiedBy = objData.ManagedBy,
                                objData.CampaignDurationId
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

        public List<CampaignDurationModal> GetCampaignDurationList()
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    string query = "Select CampaignDurationId, Duration, FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from CampaignDuration order by CampaignDurationId desc";
                    List<CampaignDurationModal> _objData = _dbDapperContext.Query<CampaignDurationModal>(query).ToList();

                    return _objData;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public CampaignDurationModal GetCampaignDurationById(int CampaignDurationId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {

                    string query = "Select CampaignDurationId, Duration from CampaignDuration where CampaignDurationId = @CampaignDurationId";
                    CampaignDurationModal _objData = _dbDapperContext.Query<CampaignDurationModal>(query, new
                    {
                        CampaignDurationId = CampaignDurationId
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

        public int RemoveCampaignDuration(int CampaignDurationId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = _dbDapperContext.Execute("Delete from CampaignDuration where CampaignDurationId = @CampaignDurationId", new
                    {
                        CampaignDurationId = CampaignDurationId
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
