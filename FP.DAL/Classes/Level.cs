using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FP.DAL
{
   public class Level : ServiceBase
    {
        public int SaveLevel(LevelModal objData)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;

                    string query = "";

                    if (objData.LevelId == 0)
                    {
                        query = "Select Count(LevelId) from Level where Title = @Title and StartRange=@StartRange and EndRange=@EndRange and IsDeleted = 0";

                        output = _dbDapperContext.Query<int>(query, new
                        {
                            objData.Title,
                            objData.StartRange,
                            objData.EndRange
                        }).FirstOrDefault();

                        if (output < 1)
                        {
                            query = "Insert into Level(Title, StartRange, EndRange) values(@Ttile, @StartRange, @EndRange)";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Title,
                                objData.StartRange,
                                objData.EndRange
                            });
                        }
                        else
                        {
                            output = 0;
                        }
                    }
                    else
                    {
                        query = "Select Count(LevelId) from Level where Title = @Title and StartRange=@StartRange and EndRange=@EndRange and IsDeleted = 0 and LevelId <> @LevelId";

                        output = _dbDapperContext.Query<int>(query, new
                        {
                            objData.Title,
                            objData.StartRange,
                            objData.EndRange,
                            objData.LevelId
                        }).FirstOrDefault();

                        if (output < 1)
                        {
                            query = "Update Level Set Title = @Title, StartRange = @StartRange, EndRange = @EndRange, IsActive=@IsActive where LevelId = @LevelId";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Title,
                                objData.StartRange,
                                objData.EndRange,
                                objData.IsActive,
                                objData.LevelId
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

        public List<LevelModal> GetLevel()
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    string query = "Select LevelId, Title, StartRange, EndRange, IsActive from Level order by LevelId desc";
                    List<LevelModal> _objData = _dbDapperContext.Query<LevelModal>(query).ToList();

                    return _objData;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public LevelModal GetLevelById(int LevelId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {

                    string query = "Select LevelId, Title, StartRange, EndRange, IsActive from Level where LevelId = @LevelId";
                    LevelModal _objData = _dbDapperContext.Query<LevelModal>(query, new
                    {
                        LevelId = LevelId
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

        public int RemoveLevel(int LevelId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = _dbDapperContext.Execute("Delete from Level where LevelId = @LevelId", new
                    {
                        LevelId = LevelId
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
