using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace FP.DAL
{
    public class Mailbox : ServiceBase
    {
        public List<MailboxModal> GetMailboxList(int MailTypeId, string MailBoxFilter, string UserId)
        {
            try
            {
                int MailBoxFilterBy = Convert.ToInt32(MailBoxFilter);
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    //#TODO: Get UserId from session or user context
                   // string UserId = "f2363ef0-c455-454c-9aa2-2cd923fb598d";
                 
                    string query = "";
                    if (MailTypeId == 0)
                    {
                        if (MailBoxFilterBy == 3)
                        {
                            query = "Select BrandMailId, Subject, left( Message,30) as Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from BrandMail where UserId=@UserId and IsDeleted !=@IsDeleted";
                            List<MailboxModal> _objData = _dbDapperContext.Query<MailboxModal>(query, new
                            {
                                UserId = UserId,
                                IsDeleted = 1
                            }).ToList();

                            return _objData;
                        }
                        else
                        {
                            query = "Select BrandMailId, Subject, left( Message,30) as Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from BrandMail where UserId=@UserId and IsDeleted !=@IsDeleted and IsRead=@IsRead";
                            List<MailboxModal> _objData = _dbDapperContext.Query<MailboxModal>(query, new
                            {
                                UserId = UserId,
                                IsDeleted = 1, 
                                IsRead= MailBoxFilterBy
                            }).ToList();

                            return _objData;
                        }
                       
                    }
                    else
                    {
                        if (MailTypeId != 6)
                        {
                            if (MailBoxFilterBy == 3)
                            {
                                query = "Select BrandMailId, Subject, left( Message,30) as Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from BrandMail where UserId=@UserId and MailTypeId=@MailTypeId and IsDeleted !=@IsDeleted ";
                                List<MailboxModal> _objData = _dbDapperContext.Query<MailboxModal>(query, new
                                {
                                    UserId = UserId,
                                    MailTypeId = MailTypeId,
                                    IsDeleted = 1,
                                }).ToList();

                                return _objData;
                            }
                            else { 
                            query = "Select BrandMailId, Subject, left( Message,30) as Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from BrandMail where UserId=@UserId and MailTypeId=@MailTypeId and IsDeleted !=@IsDeleted and IsRead=@IsRead";
                            List<MailboxModal> _objData = _dbDapperContext.Query<MailboxModal>(query, new
                            {
                                UserId = UserId,
                                MailTypeId = MailTypeId,
                                IsDeleted = 1,
                                IsRead = MailBoxFilterBy
                            }).ToList();

                            return _objData;
                        }
                        }
                        else
                        {
                            if (MailBoxFilterBy == 3)
                            {
                                query = "Select BrandMailId, Subject, left( Message,30) as Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from BrandMail where MailFrom=@MailFrom and IsDeleted !=@IsDeleted";
                                List<MailboxModal> _objData = _dbDapperContext.Query<MailboxModal>(query, new
                                {
                                    MailFrom = UserId,
                                    IsDeleted = 1
                                }).ToList();
                                return _objData;
                            }
                            else
                            {
                                query = "Select BrandMailId, Subject, left( Message,30) as Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from BrandMail where MailFrom=@MailFrom and IsDeleted !=@IsDeleted and IsRead=@IsRead";
                                List<MailboxModal> _objData = _dbDapperContext.Query<MailboxModal>(query, new
                                {
                                    MailFrom = UserId,
                                    IsDeleted = 1,
                                    IsRead = MailBoxFilterBy
                                }).ToList();
                                return _objData;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public MailboxModal GetMailboxById(int MailboxId, string UserId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    //#TODO: Get UserId from session or user context
                    //string UserId = "f2363ef0-c455-454c-9aa2-2cd923fb598d";
                    int BrandMailId = MailboxId;
                    
                    string  query = "Update BrandMail set IsRead=@IsRead where BrandMailId=@BrandMailId and UserId=@UserId";
                    MailboxModal _objData = _dbDapperContext.Query<MailboxModal>(query, new
                    {
                        UserId = UserId,
                        BrandMailId = BrandMailId,
                        IsRead = 1
                    }).FirstOrDefault();

                   // query = "Select BrandMailId, Subject, Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate, MailFrom, MailTypeId from BrandMail where  BrandMailId=@BrandMailId and IsDeleted !=@IsDeleted";
                    query = "Select BrandMailId, Subject, Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate, Email as MailFrom, MailTypeId, ProjectProposalId, ProjectProposalUpdateId from BrandMail INNER JOIN  AspNetUsers on AspNetUsers.Id=MailFrom where  BrandMailId=@BrandMailId and IsDeleted !=@IsDeleted";

                    _objData = _dbDapperContext.Query<MailboxModal>(query, new
                    {
                      //  UserId = UserId,
                        BrandMailId= BrandMailId,
                        IsDeleted = 1
                    }).FirstOrDefault();

                    if (_objData.ProjectProposalId != "" && _objData.ProjectProposalId != null)
                    {
                        query = "select ProjectProposal.ProjectProposalId, CampaignId, ProjectDescription,CASE WHEN  PaymentType=1 THEN 'FixedCost' ELSE 'Milestone' END as PaymentType, NoOfMilestone, " +
                                "Milestone1, Milestone2, Milestone3, Status, ProjectProposal.UserId, Approved, Milestone1Amount, Milestone2Amount, Milestone3Amount, " + 
                                "Milestone, ProposalAmount, ReceivedAmount, ProposalDate, BrandMailId, Subject, Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate, "+ 
                                " Email as MailFrom, MailTypeId from ProjectProposal inner join BrandMail on BrandMail.ProjectProposalId = ProjectProposal.ProjectProposalId INNER JOIN AspNetUsers on AspNetUsers.Id = MailFrom where BrandMailId = @BrandMailId and IsDeleted != @IsDeleted";

                        _objData = _dbDapperContext.Query<MailboxModal>(query, new
                        {
                            //  UserId = UserId,
                            BrandMailId = BrandMailId,
                            IsDeleted = 1
                        }).FirstOrDefault();
                    }
                    else if (_objData.ProjectProposalUpdateId != "" && _objData.ProjectProposalUpdateId != null)
                    {
                        query = "select  ProjectProposalUpdateId, " +
                               "Url, ProjectProposalUpdate.UserId, " +
                               " BrandMailId, Subject, Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate," +
                               " Email as MailFrom, MailTypeId, ProjectProposalUpdate.IsApproved As Status from ProjectProposalUpdate inner join BrandMail on BrandMail.ProjectProposalUpdateId = ProjectProposalUpdate.Id INNER JOIN AspNetUsers on AspNetUsers.Id = MailFrom where BrandMailId = @BrandMailId and IsDeleted != @IsDeleted";

                        _objData = _dbDapperContext.Query<MailboxModal>(query, new
                        {
                            //  UserId = UserId,
                            BrandMailId = BrandMailId,
                            IsDeleted = 1
                        }).FirstOrDefault();
                    }

                    return _objData;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }


        public int RemoveMailboxs(int MailboxId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = _dbDapperContext.Execute("update BrandMail set IsDeleted=@IsDeleted, DeleteDate=@DeleteDate where BrandMailId=@BrandmailId", new
                    {
                        BrandMailId = MailboxId,
                        IsDeleted = 1,
                        DeleteDate=DateTime.Now
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


        public int ReplyMailbox(MailboxModal objData, string UserId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;

                    string query = "";

                    if (objData.BrandMailId == 0)
                    {
                        
                    }
                    else
                    {
                        string query1 = "Select BrandMailId, Subject, Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate, MailFrom, UserId, MailTypeId from BrandMail where  BrandMailId=@BrandMailId and IsDeleted !=@IsDeleted";
                        var _objData = _dbDapperContext.Query<MailboxModal>(query1, new
                        {
                            //  UserId = UserId,
                            BrandMailId =objData.BrandMailId,
                            IsDeleted = 1
                        }).FirstOrDefault();

                        query = @"Insert into BrandMail(MailTypeId,Subject, Message, CreatedDate, MailFrom, UserId, IsDeleted, IsRead, PreviousBrandMailId) values(@MailTypeId,@Subject, @Message, GetUtcDate(),@MailFrom,@UserId, @IsDeleted, @IsRead, @PreviousBrandMailId); SELECT CAST(SCOPE_IDENTITY() as int)";

                        var id = _dbDapperContext.Query<int>(query, new
                        {
                            objData.MailTypeId,
                            objData.Subject,
                            objData.Message,
                            MailFrom = _objData.UserId,
                            UserId = _objData.MailFrom,
                            IsDeleted = 0,
                            IsRead = 0,
                            PreviousBrandMailId= objData.BrandMailId
                        }).Single();

                        output = id;

                       
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

        public int SaveMailBoxFile(int id, string fileName, string filePath )
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;
                    var query = @"Insert into BrandMailFile(BrandMailId,FilePath, FileName) values(@BrandMailId,@FilePath, @FileName); SELECT CAST(SCOPE_IDENTITY() as int)"; 
                    output = _dbDapperContext.Query<int>(query, new
                    {
                        BrandMailId = id,
                        FilePath= filePath,
                        FileName= fileName
                    }).Single(); 

                    output = id;
                    return output;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public List<MailboxModal> GetMailBoxByText(string SearchMailBoxByText, string MailTypeId, string MailBoxFilter)
        {
           
                try
                {
                    string SearchText = "%"+SearchMailBoxByText+"%";
                    int MailBoxFilterBy = Convert.ToInt32(MailBoxFilter);
                    using (IDbConnection _dbDapperContext = GetDefaultConnection())
                    {
                        //#TODO: Get UserId from session or user context
                        string UserId = "f2363ef0-c455-454c-9aa2-2cd923fb598d";

                        string query = "";
                        if (MailTypeId == "0")
                        {
                            if (MailBoxFilterBy == 3)
                            {
                                query = "Select BrandMailId, Subject, left( Message,30) as Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from BrandMail where UserId=@UserId and IsDeleted !=@IsDeleted and Message like @Message or Subject like  @Subject";
                                List<MailboxModal> _objData = _dbDapperContext.Query<MailboxModal>(query, new
                                {
                                    UserId = UserId,
                                    IsDeleted = 1,
                                    Message = SearchText,
                                    Subject = SearchText
                                }).ToList();

                                return _objData;
                            }
                            else
                            {
                                query = "Select BrandMailId, Subject, left( Message,30) as Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from BrandMail where UserId=@UserId and IsDeleted !=@IsDeleted and IsRead=@IsRead and Message like @Message or Subject like  @Subject";
                                List<MailboxModal> _objData = _dbDapperContext.Query<MailboxModal>(query, new
                                {
                                    UserId = UserId,
                                    IsDeleted = 1,
                                    IsRead = MailBoxFilterBy,
                                    Message = SearchText,
                                    Subject = SearchText
                                }).ToList();

                                return _objData;
                            }

                        }
                        else
                        {
                            if (MailTypeId != "6")
                            {
                                if (MailBoxFilterBy == 3)
                                {
                                    query = "Select BrandMailId, Subject, left( Message,30) as Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from BrandMail where UserId=@UserId and MailTypeId=@MailTypeId and IsDeleted !=@IsDeleted and Message like @Message or Subject like  @Subject";
                                    List<MailboxModal> _objData = _dbDapperContext.Query<MailboxModal>(query, new
                                    {
                                        UserId = UserId,
                                        MailTypeId = MailTypeId,
                                        IsDeleted = 1,
                                        Message = SearchText,
                                        Subject = SearchText
                                    }).ToList();

                                    return _objData;
                                }
                                else
                                {
                                    query = "Select BrandMailId, Subject, left( Message,30) as Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from BrandMail where UserId=@UserId and MailTypeId=@MailTypeId and IsDeleted !=@IsDeleted and IsRead=@IsRead and Message like @Message or Subject like  @Subject";
                                    List<MailboxModal> _objData = _dbDapperContext.Query<MailboxModal>(query, new
                                    {
                                        UserId = UserId,
                                        MailTypeId = MailTypeId,
                                        IsDeleted = 1,
                                        IsRead = MailBoxFilterBy,
                                        Message = SearchText,
                                        Subject = SearchText
                                    }).ToList();

                                    return _objData;
                                }
                            }
                            else
                            {
                                if (MailBoxFilterBy == 3)
                                {
                                    query = "Select BrandMailId, Subject, left( Message,30) as Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from BrandMail where MailFrom=@MailFrom and IsDeleted !=@IsDeleted and Message like @Message or Subject like  @Subject";
                                    List<MailboxModal> _objData = _dbDapperContext.Query<MailboxModal>(query, new
                                    {
                                        MailFrom = UserId,
                                        IsDeleted = 1,
                                        Message = SearchText,
                                        Subject = SearchText
                                    }).ToList();
                                    return _objData;
                                }
                                else
                                {
                                    query = "Select BrandMailId, Subject, left( Message,30) as Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from BrandMail where MailFrom=@MailFrom and IsDeleted !=@IsDeleted and IsRead=@IsRead and Message like @Message or Subject like  @Subject";
                                    List<MailboxModal> _objData = _dbDapperContext.Query<MailboxModal>(query, new
                                    {
                                        MailFrom = UserId,
                                        IsDeleted = 1,
                                        IsRead = MailBoxFilterBy,
                                        Message = SearchText,
                                        Subject = SearchText
                                    }).ToList();
                                    return _objData;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    return null;
                }
           
        }

        public List<BrandMailFile> GetDocument(int MailId)
        {
            try
            {
                int BrandMailId = Convert.ToInt32(MailId);
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    //#TODO: Get UserId from session or user context
                    // string UserId = "f2363ef0-c455-454c-9aa2-2cd923fb598d";

                    string query = "";

                    query = "Select ProjectProposalId, ProjectProposalUpdateId  from BrandMail where  BrandMailId =@BrandMailId";
                    var _objData1 = _dbDapperContext.Query<MailboxModal>(query, new
                    {
                        BrandMailId = BrandMailId
                    }).FirstOrDefault();

                    //if (_objData1 != null)
                    //{
                        if (_objData1.ProjectProposalId != null)
                        {
                            int projectProposalId = Convert.ToInt32(_objData1.ProjectProposalId);
                            query = "Select BrandMailFileId, BrandMailId, FileName, FilePath From BrandMailFile where ProjectProposalId =@ProjectProposalId";
                            List<BrandMailFile> _objData = _dbDapperContext.Query<BrandMailFile>(query, new
                            {
                                ProjectProposalId = projectProposalId
                            }).ToList();

                            return _objData;
                        }
                        else if (_objData1.ProjectProposalUpdateId != null)
                        {
                            int ProjectProposalUpdateId = Convert.ToInt32(_objData1.ProjectProposalUpdateId);
                            query = "Select FileName, FilePath From ProjectProposalUpdatesFile where ProjectProposalUpdateId =@ProjectProposalUpdateId";
                            List<BrandMailFile> _objData = _dbDapperContext.Query<BrandMailFile>(query, new
                            {
                               ProjectProposalUpdateId
                            }).ToList();

                            return _objData;
                        }
                    //}
                    else
                    {
                        query = "Select BrandMailFileId, BrandMailId, FileName, FilePath From BrandMailFile where BrandMailId =@BrandMailId";
                        List<BrandMailFile> _objData = _dbDapperContext.Query<BrandMailFile>(query, new
                        {
                            BrandMailId
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


        public List<BrandMailFile> GetDocumentForDownload(int BrandMailFileId)
        {
           // byte fileData ;
            try
            {
               // int BrandMailFileId = Convert.ToInt32(BrandMailFileId);
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    //#TODO: Get UserId from session or user context
                    // string UserId = "f2363ef0-c455-454c-9aa2-2cd923fb598d";

                    string query = "";


                    query = "Select FileName,FileData From BrandMailFile where BrandMailFileId =@BrandMailFileId";
                    List<BrandMailFile> _objData = _dbDapperContext.Query<BrandMailFile>(query, new
                    {
                        BrandMailFileId = BrandMailFileId
                    }).ToList();
                    //fileData = _dbDapperContext.Query<byte>(query, new
                    //{
                    //    BrandMailFileId
                    //}).Single();

                    return _objData;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }


        public int ApproveProposal(int MailboxId, string UserId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = -1;

                    string query = "";

                    if (MailboxId == 0)
                    {

                    }
                    else
                    {
                        string query1 = "Select ProjectProposalId from BrandMail where  BrandMailId=@BrandMailId and IsDeleted !=@IsDeleted";
                        var _objData = _dbDapperContext.Query<MailboxModal>(query1, new
                        {
                            //  UserId = UserId,
                            BrandMailId = MailboxId,
                            IsDeleted = 1
                        }).FirstOrDefault();

                        int projectProposalId = Convert.ToInt16( _objData.ProjectProposalId);
                        //if(_objData.ProjectProposalId)
                        // query = @"Insert into BrandMail(MailTypeId,Subject, Message, CreatedDate, MailFrom, UserId, IsDeleted, IsRead) values(@MailTypeId,@Subject, @Message, GetUtcDate(),@MailFrom,@UserId, @IsDeleted, @IsRead); SELECT CAST(SCOPE_IDENTITY() as int)";
                        query = "Update ProjectProposal Set Approved = @Approved where ProjectProposalId = @ProjectProposalId";
                        output = _dbDapperContext.Execute(query, new
                        {
                            Approved = 1,
                            ProjectProposalId= projectProposalId
                        });

                         query = "Select CampaignId from ProjectProposal where  ProjectProposalId=@ProjectProposalId ";
                           var outputData = _dbDapperContext.Query<ProjectsProposalModal>(query, new
                            {
                                ProjectProposalId= projectProposalId
                           }).FirstOrDefault();

                        int CamapignId = Convert.ToInt16(outputData.CampaignId);
                        query = "Update ProjectProposal Set Approved = @Approved where ProjectProposalId != @ProjectProposalId and CampaignId=@CampaignId";
                        output = _dbDapperContext.Execute(query, new
                        {
                            Approved = 0,
                            ProjectProposalId= projectProposalId,
                            CampaignId = CamapignId
                        });

                        query = "Update BrandMail Set MailTypeId = @MailTypeId where ProjectProposalId = @ProjectProposalId and  BrandMailId=@BrandMailId";
                        output = _dbDapperContext.Execute(query, new
                        {
                            MailTypeId = 2,
                            ProjectProposalId = projectProposalId,
                            BrandMailId = MailboxId,
                        });


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


        public List<BrandMailFile> DeleteDocument(int BrandMailFileId, string UserId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    string query = "Select BrandMailId From BrandMailFile where BrandMailFileId =@BrandMailFileId";
                    var _obj = _dbDapperContext.Query<BrandMailFile>(query, new
                    {
                         BrandMailFileId
                    }).SingleOrDefault();

                    int brandMailId= Convert.ToInt32(_obj.BrandMailId);

                    int output = _dbDapperContext.Execute("Delete from BrandMailFile where BrandMailFileId=@BrandMailFileId", new
                    {
                         BrandMailFileId
                    });

                    query = "Select BrandMailFileId, BrandMailId, FileName, FilePath From BrandMailFile where BrandMailId =@BrandMailId";
                    List<BrandMailFile> _objFinalData = _dbDapperContext.Query<BrandMailFile>(query, new
                    {
                        BrandMailId = brandMailId
                    }).ToList();

                    return _objFinalData;
                   
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }


        public int SendProposalUpdate(int MailboxId, int UpdateValue, string UserId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = -1;

                    string query = "";
                  
                        string query1 = "Select ProjectProposalUpdateId from BrandMail where  BrandMailId=@BrandMailId and IsDeleted !=@IsDeleted";
                        var _objData = _dbDapperContext.Query<MailboxModal>(query1, new
                        {
                            //  UserId = UserId,
                            BrandMailId = MailboxId,
                            IsDeleted = 1
                        }).FirstOrDefault();

                        int projectProposalUpdateId = Convert.ToInt16(_objData.ProjectProposalUpdateId);
                        //if(_objData.ProjectProposalId)
                        // query = @"Insert into BrandMail(MailTypeId,Subject, Message, CreatedDate, MailFrom, UserId, IsDeleted, IsRead) values(@MailTypeId,@Subject, @Message, GetUtcDate(),@MailFrom,@UserId, @IsDeleted, @IsRead); SELECT CAST(SCOPE_IDENTITY() as int)";
                        query = "Update ProjectProposalUpdate Set IsApproved = @Approved where Id = @ProjectProposalUpdateId";
                        output = _dbDapperContext.Execute(query, new
                        {
                            Approved = UpdateValue,
                            ProjectProposalUpdateId = projectProposalUpdateId
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
