using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FP.DAL
{
    public class AudienceAge : ServiceBase
    {


        public int SaveAudienceAge(AudienceAgeModal objData)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;

                    string query = "";

                    if (objData.AudienceAgeId == 0)
                    {
                        query = "Select Count(AudienceAgeId) from AudienceAge where Title = @Title and IsDeleted = 0";

                        output = _dbDapperContext.Query<int>(query, new
                        {
                            objData.Title
                        }).FirstOrDefault();

                        if (output < 1)
                        {
                            query = "Insert into AudienceAge(Title, MinValue, MaxValue, Duration, CreatedBy, CreatedDate) values(@Title,  @MinValue, @MaxValue, @Duration, @CreatedBy, GetUtcDate())";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Title,
                                objData.MinValue,
                                objData.MaxValue,
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
                        query = "Select Count(AudienceAgeId) from AudienceAge where Title = @Title and IsDeleted = 0 and AudienceAgeId <> @AudienceAgeId";

                        output = _dbDapperContext.Query<int>(query, new
                        {
                            objData.Title,
                            objData.AudienceAgeId
                        }).FirstOrDefault();

                        if (output < 1)
                        {
                            query = "Update AudienceAge Set Title = @Title, MinValue = @MinValue, MaxValue = @MaxValue, Duration = @Duration, ModifiedBy = @ModifiedBy, ModifiedDate = GetUtcDate() where AudienceAgeId = @AudienceAgeId";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Title,
                                objData.MinValue,
                                objData.MaxValue,
                                objData.Duration,
                                ModifiedBy = objData.ManagedBy,
                                objData.AudienceAgeId
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

        public List<AudienceAgeModal> GetAudienceAgeList()
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    string query = "Select AudienceAgeId, Title, MinValue, MaxValue, Duration, FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from AudienceAge order by AudienceAgeId desc";
                    List<AudienceAgeModal> _objData = _dbDapperContext.Query<AudienceAgeModal>(query).ToList();

                    return _objData;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public AudienceAgeModal GetAudienceAgeById(int AudienceAgeId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {

                    string query = "Select AudienceAgeId, Title, MinValue, MaxValue, Duration  from AudienceAge where AudienceAgeId = @AudienceAgeId";
                    AudienceAgeModal _objData = _dbDapperContext.Query<AudienceAgeModal>(query, new
                    {
                        AudienceAgeId = AudienceAgeId
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

        public int RemoveAudienceAge(int AudienceAgeId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = _dbDapperContext.Execute("Delete from AudienceAge where AudienceAgeId = @AudienceAgeId", new
                    {
                        AudienceAgeId = AudienceAgeId
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


        public List<ClientModal> GetAllClient()
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    string query = @"SELECT AspNetUsers.Id, AspNetUsers.Email, CASE WHEN AspNetUsers.IsApproved=1 THEN 'ACTIVE' ELSE 'INACTIVE' END AS Status,
                                        CASE WHEN AspNetUsers.IsApproved=1 THEN 'INACTIVE' ELSE 'ACTIVE' END AS Edit  FROM AspNetUsers
                                        INNER JOIN  AspNetUserRoles ON AspNetUserRoles.UserId=AspNetUsers.Id and AspNetUserRoles.RoleId=2";
                    List<ClientModal> _objData = _dbDapperContext.Query<ClientModal>(query).ToList();

                    return _objData;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public int EditClient(string Id, string Type)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;

                    string query = "";
                    int updateValue = 0;
                    if (Type == "ACTIVE")
                    {
                        updateValue = 1;
                    }
                    

                        
                    query = "Update AspNetUsers Set IsApproved = @IsApproved where Id = @Id";
                    output = _dbDapperContext.Execute(query, new
                    {
                        IsApproved = updateValue,
                                Id
                    }); ;
                       
                   
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
