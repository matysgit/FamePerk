using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FP.DAL
{
    public class Channel : ServiceBase
    {


        public int SaveChannel(ChannelModal objData)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;

                    string query = "";

                    if (objData.ChannelId == 0)
                    {
                        query = "Select Count(ChannelId) from Channel where Name = @Name and IsDeleted = 0";

                        output = _dbDapperContext.Query<int>(query, new
                        {
                            objData.Name
                        }).FirstOrDefault();

                        if (output < 1)
                        {
                            query = "Insert into Channel(Name, CreatedBy, CreatedDate) values(@Name, @CreatedBy, GetUtcDate())";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Name,
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
                        query = "Select Count(ChannelId) from Channel where Name = @Name and IsDeleted = 0 and ChannelId <> @ChannelId";

                        output = _dbDapperContext.Query<int>(query, new
                        {
                            objData.Name,
                            objData.ChannelId
                        }).FirstOrDefault();

                        if (output < 1)
                        {
                            query = "Update Channel Set Name = @Name, ModifiedBy = @ModifiedBy, ModifiedDate = GetUtcDate() where ChannelId = @ChannelId";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Name,
                                ModifiedBy = objData.ManagedBy,
                                objData.ChannelId
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

        public List<ChannelModal> GetChannelList()
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    string query = "Select ChannelId, Name, FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from Channel order by ChannelId desc";
                    List<ChannelModal> _objData = _dbDapperContext.Query<ChannelModal>(query).ToList();

                    return _objData;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public ChannelModal GetChannelById(int ChannelId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {

                    string query = "Select ChannelId, Name from Channel where ChannelId = @ChannelId";
                    ChannelModal _objData = _dbDapperContext.Query<ChannelModal>(query, new
                    {
                        ChannelId = ChannelId
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

        public int RemoveChannel(int ChannelId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = _dbDapperContext.Execute("Delete from Channel where ChannelId = @ChannelId", new
                    {
                        ChannelId = ChannelId
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
