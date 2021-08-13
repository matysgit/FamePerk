using Dapper;
using FP.DAL.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;

namespace FP.DAL
{
  public  class Creator : ServiceBase
    {

        public string GetClientDetail(string userId)
        {
            string output = "";
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                     string query = @"Select IsApproved From AspNetUsers Where Id=@UserId";
                   
                    CreatorModal _objData = _dbDapperContext.Query<CreatorModal>(query, new
                    {
                        UserId = userId
                    }).FirstOrDefault();
                    output = _objData.IsApproved;
                }
            }
            catch
            {

            }
            return output;
        }
    
        public int SaveCreator(CreatorModal objData, string UserId, string fileName, string currencyType)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;

                    string query = "";

                     output = _dbDapperContext.Execute("Delete  from CreatorProfile Where UserId=@UserId", new
                    {
                        UserId = UserId
                    });


                    query = @"Insert into CreatorProfile(FullName, ContactNumber, State, YouTube, Instagram, Facebook, MinimumBudgetedProject, PastWorkExperience, Summary, TargetAudience, UserId, CountryId, ProfileImage, DOB, Language, Categories, Gender, CurrencyType) 
                            values(@FullName, @ContactNumber, @State, @YouTube, @Instagram, @Facebook, @MinimumBudgetedProject,  @PastWorkExperience, @Summary, @TargetAudience, @UserId, @CountryId, @ProfileImage, @DOB, @Language, @Categories, @Gender, @CurrencyType)";

                    output = _dbDapperContext.Execute(query, new
                    {
                        objData.FullName,
                        objData.ContactNumber,
                        objData.State,
                        objData.YouTube,
                        objData.Instagram,
                        objData.Facebook,
                        objData.MinimumBudgetedProject, 
                        objData.PastWorkExperience,
                        objData.Summary,
                        objData.TargetAudience,
                        UserId=UserId,
                        objData.CountryId ,
                        ProfileImage = fileName,

                        objData.DOB ,
                        objData.Language,
                        objData.Categories,
                        objData.Gender,
                        CurrencyType= currencyType

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

        public int InsertCreator(string name, string userId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;

                    string query = "";

                    output = _dbDapperContext.Execute("Delete  from CreatorProfile Where UserId=@UserId", new
                    {
                        UserId = userId
                    });


                    query = @"Insert into CreatorProfile(FullName, UserId) 
                            values(@FullName, @UserId)";

                    output = _dbDapperContext.Execute(query, new
                    {
                        FullName=name,
                        UserId = userId


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
        public CreatorModal GetCreatorInfo(string UserId,string currencyType)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                   
                  string  query = "Select CreatorProfile.FullName, ContactNumber, State, CountryId, YouTube, Instagram, Facebook, " +
                        "CategoryId, MinimumBudgetedProject, PastWorkExperience, Summary, TargetAudience, Email," +
                        "FORMAT(CreatorProfile.DOB, 'MM/dd/yyyy ') as  DOB, Language, Categories, Gender,CurrencyType from AspNetUsers left join CreatorProfile on CreatorProfile.UserId = AspNetUsers.Id where Id = @Id";

                   CreatorModal _objData = _dbDapperContext.Query<CreatorModal>(query, new
                    {
                          Id = UserId
                    }).FirstOrDefault();

                    if (_objData.CurrencyType != null)
                    {
                        float exchangeRate = CurrencyConverter.GetExchangeRate(_objData.CurrencyType, currencyType, 1);
                        if (_objData.MinimumBudgetedProject != null)
                        {
                            double amount = exchangeRate * Convert.ToDouble(_objData.MinimumBudgetedProject);
                            _objData.MinimumBudgetedProject = (amount).ToString("0.##");
                        }
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

        public List<CountryModal> GetCountry()
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {

                    //string query = "Select CountryId , Name , IsActive, CreatedBy, CreatedDate, IsDeleted, DeletedBy, DeletedDate  from Country";
                    string query = "Select CountryId as id , Name as label, CountryId, Name  from Country";

                    List<CountryModal> _objData = _dbDapperContext.Query<CountryModal>(query, new
                    {
                       
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

        public List<CreatorFeedbackModal> GetCreatorFeedBack(string UserId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {

                    string query = @"SELECT TOP (10) Id, CreatorId, Feedback, Date
                                        FROM CreatorFeedback WHERE CreatorId=@CreatorId";

                    List<CreatorFeedbackModal> _objData = _dbDapperContext.Query<CreatorFeedbackModal>(query, new
                    {
                        CreatorId= UserId
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


        public List<CreatorModal> GetCreatorByFillter(CreatorFillterModal obj)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    //#TODO: Get UserId from session or user context
                    // string UserId = "f2363ef0-c455-454c-9aa2-2cd923fb598d";

                    string query = "";
                    Nullable<int> countryId = null;
                   // int CountyId = null;
                    string ageId = "";
                  
                    if (obj.CountyId != "0" && obj.CountyId != null)
                    {
                        countryId = Convert.ToInt32(obj.CountyId);
                    }
                    if (obj.TargetAudienceId != "0" && obj.TargetAudienceId !=null)
                    {
                        ageId = obj.TargetAudienceId;
                    }
                    if(ageId =="" && countryId == null)
                    {
                        query = @"SELECT CreatorId, UserId, FullName, ContactNumber, State, CountryId, YouTube, Instagram, Facebook, CategoryId, MinimumBudgetedProject, PastWorkExperience, 
                                Summary, TargetAudience, ProfileImage, DATEDIFF(hour, CreatorProfile.DOB, GETDATE()) / 8766 AS CurrentAge, Language,
                             Categories, Gender FROM CreatorProfile INNER JOIN AspNetUsers on CreatorProfile.UserId = AspNetUsers.Id";
                        List<CreatorModal> _objData = _dbDapperContext.Query<CreatorModal>(query, new
                        {
                           
                        }).ToList();

                        List<CreatorModal> objFilnalCreatorData = GetCreatorDetails(_objData);
                        return objFilnalCreatorData;
                       
                    }
                   else if ( ageId =="")
                    {
                        query = @"SELECT CreatorId, UserId, FullName, ContactNumber, State, CountryId, YouTube, Instagram, Facebook, CategoryId, MinimumBudgetedProject, PastWorkExperience, 
                                Summary, TargetAudience, ProfileImage, DATEDIFF(hour, CreatorProfile.DOB, GETDATE()) / 8766 AS CurrentAge, Language,
                             Categories, Gender  FROM CreatorProfile INNER JOIN AspNetUsers on CreatorProfile.UserId = AspNetUsers.Id WHERE CountryId = isNULL(@CountryId, CountryId)";
                        List<CreatorModal> _objData = _dbDapperContext.Query<CreatorModal>(query, new
                        {
                            CountryId = countryId
                        }).ToList();

                     List<CreatorModal> objFilnalCreatorData=   GetCreatorDetails(_objData);
                        return objFilnalCreatorData;
                    }
                    else
                    {
                        query = @"SELECT CreatorId, UserId, FullName, ContactNumber, State, CountryId, YouTube, Instagram, Facebook, CategoryId, MinimumBudgetedProject, PastWorkExperience, 
                                Summary, TargetAudience, ProfileImage, DATEDIFF(hour, CreatorProfile.DOB, GETDATE()) / 8766 AS CurrentAge, Language,
                             Categories, Gender  FROM CreatorProfile INNER JOIN AspNetUsers on CreatorProfile.UserId = AspNetUsers.Id WHERE CountryId = isNULL(@CountryId, CountryId) and TargetAudience LIKE @TargetAudience ";
                        List<CreatorModal> _objData = _dbDapperContext.Query<CreatorModal>(query, new
                        {
                            CountryId = countryId,
                            TargetAudience = "%" + ageId + "%"// ageId

                        }).ToList();

                        List<CreatorModal> objFilnalCreatorData = GetCreatorDetails(_objData);
                        return objFilnalCreatorData;
                    }
                   

                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public List<CreatorModal> GetCreatorDetails(List<CreatorModal> objData)
        {
            foreach (var objList in objData)
            {
                var youTubeLink = objList.YouTube.ToString();
                //  string[] youTubeIds = youTubeLink.Split("/");
                List<string> youTubeIds = new List<string>(
                             youTubeLink.Split(new string[] { "/" }, StringSplitOptions.None));
                if (youTubeIds.Count > 4)
                {
                    //https://api.instagram.com/v1/users/{user-id}/follows?access_token=ACCESS-TOKEN
                    var youTubeId = youTubeIds[4];
                    var api = "AIzaSyDB3tjtbUZNKcraqOhvMMC-HAeJ3yXYvxw";
                    var url = "https://www.googleapis.com/youtube/v3/channels?part=statistics&id=" + youTubeId + "&key=" + api;
                    WebRequest request = HttpWebRequest.Create(url);
                    request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    WebResponse response = request.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string responseText = reader.ReadToEnd();
                    dynamic data = JObject.Parse(responseText);
                    objList.YouTube = FormatNumber(Convert.ToInt32(data.items[0].statistics.subscriberCount));
                }
                else
                {
                    objList.YouTube = "NA";
                }
            }
            return objData;
        }
        static string FormatNumber(int num)
        {

            if (num >= 100000000)
                return (num / 1000000).ToString("#,0M");

            if (num >= 10000000)
                return (num / 1000000).ToString("0.#") + "M";

            if (num >= 100000)
                return (num / 1000).ToString("#,0K");

            if (num >= 10000)
                return (num / 1000).ToString("0.#") + "K";

            return num.ToString("#,0");
        }

        public CreatorModal GetCreatorInfoMail(string email)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {

                    string query = "SELECT * FROM CreatorProfile INNER join AspNetUsers on  AspNetUsers.id = CreatorProfile.UserId WHERE Email = @Email";

                    CreatorModal _objData = _dbDapperContext.Query<CreatorModal>(query, new
                    {
                        Email = email
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

        #region Mailbox
        public List<MailboxModal> GetMailboxList(int MailTypeId, string MailBoxFilter, string UserId)
        {
            try
            {
                int MailBoxFilterBy = Convert.ToInt32(MailBoxFilter);
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    //#TODO: Get UserId from session or user context
                    //string UserId = "f2363ef0-c455-454c-9aa2-2cd923fb598d";

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
                                IsRead = MailBoxFilterBy
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
                            else
                            {
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

                    string query = "Update BrandMail set IsRead=@IsRead where BrandMailId=@BrandMailId and UserId=@UserId";
                    MailboxModal _objData = _dbDapperContext.Query<MailboxModal>(query, new
                    {
                        UserId = UserId,
                        BrandMailId = BrandMailId,
                        IsRead = 1
                    }).FirstOrDefault();

                    // query = "Select BrandMailId, Subject, Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate, MailFrom, MailTypeId from BrandMail where  BrandMailId=@BrandMailId and IsDeleted !=@IsDeleted";
                    query = "Select BrandMailId, Subject, Message,  FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate, Email as MailFrom, MailTypeId from BrandMail INNER JOIN  AspNetUsers on AspNetUsers.Id=MailFrom where  BrandMailId=@BrandMailId and IsDeleted !=@IsDeleted";

                    _objData = _dbDapperContext.Query<MailboxModal>(query, new
                    {
                        //  UserId = UserId,
                        BrandMailId = BrandMailId,
                        IsDeleted = 1
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
                        DeleteDate = DateTime.Now
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


        public int ReplyMailbox(MailboxModal objData)
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
                            BrandMailId = objData.BrandMailId,
                            IsDeleted = 1
                        }).FirstOrDefault();

                        query = "Insert into BrandMail(MailTypeId,Subject, Message, CreatedDate, MailFrom, UserId, IsDeleted, IsRead) values(@MailTypeId,@Subject, @Message, GetUtcDate(),@MailFrom,@UserId, @IsDeleted, @IsRead)";
                        //"Update BrandMail Set Name = @Name, ModifiedBy = @ModifiedBy, ModifiedDate = GetUtcDate() where ProductCategoryId = @ProductCategoryId";
                        output = _dbDapperContext.Execute(query, new
                        {
                            objData.MailTypeId,
                            objData.Subject,
                            objData.Message,
                            MailFrom = _objData.UserId,
                            UserId = _objData.MailFrom,
                            IsDeleted = 0,
                            IsRead = 0
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

        public List<MailboxModal> GetMailBoxByText(string SearchMailBoxByText, string MailTypeId, string MailBoxFilter)
        {

            try
            {
                string SearchText = "%" + SearchMailBoxByText + "%";
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

        public MailboxModal GetUnReadMsg(string UserId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                        string query1 = "select count(*) AS Message  from BrandMail where UserId=@UserId and IsRead !=@IsDeleted";
                        var _objData = _dbDapperContext.Query<MailboxModal>(query1, new
                        {
                            
                            UserId ,
                            IsDeleted = 1
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
        #endregion

        public List<CurrencyTypeModal> GetCurrencyType()
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {

                    string query = "SELECT CurrencyId, CurrencyName FROM CurrencyType";
                   
                    List<CurrencyTypeModal> _objData = _dbDapperContext.Query<CurrencyTypeModal>(query, new
                    {

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
    }
}
