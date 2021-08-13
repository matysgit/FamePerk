using Dapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
//using System.Web.Services;
using FP.DAL.Classes;

namespace FP.DAL
{
  public  class Projects: ServiceBase
    {
        public List<CampaignModal> GetProjectList(string UserId, string convertToCurrency)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    //#TODO: Get UserId from session or user context
                    // string UserId = "f2363ef0-c455-454c-9aa2-2cd923fb598d";

                    string query = "";

                    query = "SELECT Campaign.CampaignId, ProductCategory.Name AS ProductCategory, " +
                      "CampaignTitle,  Duration AS CampaignDuration, CASE WHEN PrivateCampaign = 0 THEN 'Yes' ELSE 'N0' END AS PrivateCampaign , " +
                      " Budget, FORMAT(Campaign.CreatedDate, 'dd/MM/yyyy') as CreatedDate, CASE WHEN ProjectProposal.Approved = 1 THEN 'Approved' WHEN ProjectProposal.Approved = 0 THEN 'Reject' ELSE 'Active' END Approve, " +
                        "ProjectProposal.Status, Campaign.CurrencyType " +
                      "FROM  Campaign " +
                      "INNER JOIN ProjectProposal ON ProjectProposal.CampaignId = Campaign.CampaignId " +
                    "INNER JOIN CampaignDuration ON Campaign.CampaignDurationId = CampaignDuration.CampaignDurationId " +
                    //"INNER JOIN Budget ON Campaign.BudgetId = Budget.BudgetId " +
                    "INNER JOIN ProductCategory ON Campaign.ProductCategoryId = ProductCategory.ProductCategoryId " +
                    "WHERE ProjectProposal.UserId= @UserId " +
                    "ORDER BY Campaign.CreatedDate DESC";


                    List<CampaignModal> _objData = _dbDapperContext.Query<CampaignModal>(query, new
                    {
                        UserId

                    }).ToList();


                    foreach (var objList in _objData)
                    {
                        float exchangeRate = CurrencyConverter.GetExchangeRate(objList.CurrencyType, convertToCurrency, 1);
                        if (objList.CurrencyType != null)
                        {
                            double amount = exchangeRate * Convert.ToDouble(objList.Budget);
                            objList.Budget = (amount).ToString("0.##");
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

        public List<CampaignModal> GetProjectById(int ProjectId, string UserId, string convertToCurrency)
        {
            try
            {
                CampaignListModal objModal = new CampaignListModal();
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {

                    string query = @"Select CampaignId, UserId, CampaignTypeId, SupplementalChannels,ProductCategory.ProductCategoryId, ProductCategory.Name AS ProductCategory, ProductURL, ProductPhoto, ShippingProduct, AboutYourProduct, 
                                    CampaignTitle, AboutYourBrand, CampaignGoal,CampaignDuration.CampaignDurationId , CampaignDuration.Duration AS CampaignDuration, PrivateCampaign, AudienceAgeId,  Budget, Campaign.CreatedDate, ModifyDate, Status, 
                                    AudienceGender, YouTube, YouTubeVideoType, Country,CurrencyType FROM Campaign 
                                    INNER JOIN ProductCategory ON Campaign.ProductCategoryId = ProductCategory.ProductCategoryId 
                                    INNER JOIN CampaignDuration ON Campaign.CampaignDurationId = CampaignDuration.CampaignDurationId 
                                   
                                    Where Campaign.CampaignId =@CampaignId 
                                    ";// and Status=@Status ";// INNER JOIN Budget ON Campaign.BudgetId = Budget.BudgetId 

                    List<CampaignModal> _objData = _dbDapperContext.Query<CampaignModal>(query, new
                    {
                        CampaignId = ProjectId
                    }).ToList();

                    foreach (var objList in _objData)
                    {
                        float exchangeRate = CurrencyConverter.GetExchangeRate(objList.CurrencyType, convertToCurrency, 1);
                        if (objList.CurrencyType != null)
                        {
                            double amount = exchangeRate * Convert.ToDouble(objList.Budget);
                            objList.Budget = (amount).ToString("0.##");
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


        public int SaveProposal(ProjectsProposalModal objData, string ProjectId, string userId, string currencyType)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;

                    string query = "";

                    int projectId = Convert.ToInt32(ProjectId);

                    //get Creator id
                    query = "SELECT   UserId FROM Campaign where CampaignId=@CampaignId";

                   var _objData1 = _dbDapperContext.Query<CampaignModal>(query, new
                    {
                        //UserId = userId,
                        CampaignId = ProjectId

                    }).SingleOrDefault();
                    //end
                    if (_objData1 != null)
                    {
                        var ClientId = "";
                        
                        ClientId = _objData1.UserId;

                        query = "SELECT       ProjectProposalId, CampaignId, ProjectDescription, PaymentType, Milestone1, Milestone2, Milestone3, Status, UserId, " +
                            "Approved, Milestone, ProposalAmount, ReceivedAmount, ProposalDate" +
                                   " FROM ProjectProposal Where CampaignId =@CampaignId and UserId=@UserId " + "";
                        List<ProjectsProposalModal> _objData = _dbDapperContext.Query<ProjectsProposalModal>(query, new
                        {
                            UserId = userId,
                            CampaignId = ProjectId

                        }).ToList();
                        if (_objData.Count > 0)
                        {
                           

                            query = @"Update ProjectProposal set ProjectDescription=@ProjectDescription, 
                                    PaymentType=@PaymentType, Milestone1=@Milestone1, Milestone2=@Milestone2, 
                                    Milestone3=@Milestone3, Status=@Status,   Milestone=@Milestone, 
                                    ProposalAmount=@ProposalAmount, ProposalDate=GetUtcDate(), Milestone1Amount=@Milestone1Amount,
                                    Milestone2Amount=@Milestone2Amount , Milestone3Amount=@Milestone3Amount, NoOfMilestone=@NoOfMilestone ,
                                    CurrencyType= @CurrencyType
                                    where UserId=@UserId and CampaignId=@CampaignId; ; SELECT CAST(SCOPE_IDENTITY() as int)";
                            output = _dbDapperContext.Execute(query, new
                            {
                                CampaignId = projectId,
                                objData.ProjectDescription,
                                objData.PaymentType,
                                objData.Milestone1,
                                objData.Milestone2,
                                objData.Milestone3,
                                objData.Status,
                                UserId = userId,
                                objData.Milestone,
                                objData.ProposalAmount,
                                objData.Milestone1Amount,
                                objData.Milestone2Amount,
                                objData.Milestone3Amount, 
                                objData.NoOfMilestone,
                                CurrencyType=currencyType

                            });

                            // output = _objData.ro[0]["ProjectProposalId"];
                            foreach (var data in _objData)
                            {
                                output = data.ProjectProposalId;

                            }
                        }
                        else
                        {
                            query = @"Insert into ProjectProposal
                                        (CampaignId, ProjectDescription, PaymentType, 
                                        Milestone1, Milestone2, Milestone3, Status, UserId,  Milestone, 
                                        ProposalAmount, ProposalDate, Milestone1Amount, Milestone2Amount, 
                                        Milestone3Amount, NoOfMilestone, CurrencyType) 
                                        values(@CampaignId,@ProjectDescription, @PaymentType,
                                        @Milestone1, @Milestone2, @Milestone3,@Status, @UserId, @Milestone,
                                        @ProposalAmount, GetUtcDate(), @Milestone1Amount, @Milestone2Amount, 
                                        @Milestone3Amount, @NoOfMilestone, @CurrencyType); SELECT CAST(SCOPE_IDENTITY() as int)";
                            var id = _dbDapperContext.Query<int>(query, new
                            {
                                CampaignId = projectId,
                                objData.ProjectDescription,
                                objData.PaymentType,
                                objData.Milestone1,
                                objData.Milestone2,
                                objData.Milestone3,
                                objData.Status,
                                UserId = userId,
                                objData.Milestone,
                                objData.ProposalAmount,
                                objData.Milestone1Amount,
                                objData.Milestone2Amount,
                                objData.Milestone3Amount,
                                objData.NoOfMilestone,
                                CurrencyType= currencyType
                            }).Single();

                            output = id;
                        }
                        //output = 0;
                        if (objData.Status == "Publish")
                        {
                            query = @"Insert into BrandMail(MailTypeId,Subject, Message, CreatedDate, MailFrom, UserId, IsDeleted, IsRead, ProjectProposalId) values(@MailTypeId,@Subject, @Message, GetUtcDate(),@MailFrom,@UserId, @IsDeleted, @IsRead, @ProjectProposalId); SELECT CAST(SCOPE_IDENTITY() as int)"; ;
                            
                            var id = _dbDapperContext.Query<int>(query, new
                            {
                                MailTypeId = 1,
                                Subject = "Project Proposal",
                                Message = objData.ProjectDescription,
                                MailFrom = userId,
                                UserId = ClientId,
                                IsDeleted = 0,
                                IsRead = 0,
                                ProjectProposalId = output
                            }).Single();
                        }
                        return output;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return -1;
            }
        }

        public List<ProjectsProposalModal> GetProposalByProjectId(int ProjectId, string UserId, string convertToCurrency)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {

                    string query = "SELECT       ProjectProposalId, CampaignId, ProjectDescription, PaymentType, " +
                                    "NoOfMilestone , Milestone1, Milestone2, Milestone3, Status, UserId, Approved, Milestone, " +
                                    "ProposalAmount, ReceivedAmount, ProposalDate" +
                                    ", Milestone1Amount, Milestone2Amount, Milestone3Amount, CurrencyType " +
                                    "FROM ProjectProposal Where CampaignId =@CampaignId and UserId=@UserId " + "";
                    List<ProjectsProposalModal> _objData = _dbDapperContext.Query<ProjectsProposalModal>(query, new
                    {
                        UserId = UserId,
                        CampaignId = ProjectId

                    }).ToList();

                    foreach (var objList in _objData)
                    {
                        float exchangeRate = CurrencyConverter.GetExchangeRate(objList.CurrencyType, convertToCurrency, 1);
                        if (objList.CurrencyType != null)
                        {
                            double amount = exchangeRate * Convert.ToDouble(objList.ProposalAmount);
                            objList.ProposalAmount = (amount).ToString("0.##");
                            if (objList.Milestone1Amount != null)
                            {
                                amount = exchangeRate * Convert.ToDouble(objList.Milestone1Amount);
                                objList.Milestone1Amount = (amount).ToString("0.##");
                            }
                            if (objList.Milestone2Amount != null)
                            {
                                amount = exchangeRate * Convert.ToDouble(objList.Milestone2Amount);
                                objList.Milestone2Amount = (amount).ToString("0.##");
                            }
                            if (objList.Milestone3Amount != null)
                            {
                                amount = exchangeRate * Convert.ToDouble(objList.Milestone3Amount);
                                objList.Milestone3Amount = (amount).ToString("0.##");
                            }
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

        public int SaveProposalFile(int id, string fileName, string filePath)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;
                    var query = @"Insert into BrandMailFile(ProjectProposalId,FilePath, FileName) values(@ProjectProposalId,@FilePath, @FileName); SELECT CAST(SCOPE_IDENTITY() as int)";
                    output = _dbDapperContext.Query<int>(query, new
                    {
                        ProjectProposalId = id,
                        FilePath = filePath,
                        FileName = fileName
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

        public List<CampaignModal> GetCampaignListForCreator(string convertToCurrecny)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    //#TODO: Get UserId from session or user context
                    // string UserId = "f2363ef0-c455-454c-9aa2-2cd923fb598d";

                    string query = "";

                    
                    query=  "SELECT CampaignId, UserId, CampaignTypeId, ProductCategory.Name AS ProductCategory, CASE WHEN ShippingProduct=0 THEN 'Yes' ELSE 'No' END AS ShippingProduct, " +
                            "AboutYourProduct, CampaignTitle, AboutYourBrand, Duration AS CampaignDuration, CASE WHEN PrivateCampaign = 1 THEN 'Yes' ELSE 'N0' END AS PrivateCampaign , Budget," + //Budget.Title AS Budget, " +
                            "FORMAT(Campaign.CreatedDate, 'dd/MM/yyyy ') as CreatedDate , Country AS Country,CurrencyType  " +
                            "FROM  Campaign "+
                            //"inner join Country on Country.CountryId=Campaign.CountryId  " +
                            "INNER JOIN CampaignDuration ON Campaign.CampaignDurationId = CampaignDuration.CampaignDurationId " +
                            //"INNER JOIN Budget ON Campaign.BudgetId = Budget.BudgetId "+
                            "INNER JOIN ProductCategory ON Campaign.ProductCategoryId = ProductCategory.ProductCategoryId "+
                            
                            "WHERE Status = @Status and Approved=@Approved " +
                            "ORDER BY Campaign.CreatedDate DESC";
                    
                    List<CampaignModal> _objData = _dbDapperContext.Query<CampaignModal>(query, new
                    {
                        Status = "Publish",
                        Approved=1

                    }).ToList();
                    //



                    //string to = Session["CurrencyType"].ToString();
                    foreach (var objList in _objData)
                    {
                        float exchangeRate = CurrencyConverter.GetExchangeRate(objList.CurrencyType, convertToCurrecny, 1);
                        if (objList.CurrencyType != null)
                        {
                            double amount = exchangeRate * Convert.ToDouble(objList.Budget);
                            objList.Budget = (amount).ToString("0.##");
                        }
                        
                    }
//
                    return _objData;

                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public List<CampaignModal> GetCampaignByFillter(CampaignFillterModal objData)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    //#TODO: Get UserId from session or user context
                    // string UserId = "f2363ef0-c455-454c-9aa2-2cd923fb598d";

                    string query = "";
                    string CampaignDurationId=null;
                    string BudgetId = null;
                    string CountryId = null;
                    string ProductCategoryId = null;
                    string countryName = null;
                    if(objData.ProductCategoryId != "0")
                    {
                        ProductCategoryId = objData.ProductCategoryId;
                    }
                    if (objData.CampaignDurationId != "0")
                    {
                        CampaignDurationId = objData.CampaignDurationId;
                    }
                    if (objData.BudgetId != "0")
                    {
                        BudgetId = objData.BudgetId;
                    }

                    if (objData.CountryId != "0")
                    {
                        CountryId = objData.CountryId;
                        if (CountryId !="" && CountryId != null)
                        {
                            query = "SELECT CountryId, Name FROM Country WHERE CountryId=@CountryId";
                            var _objDataCountry = _dbDapperContext.Query<CountryModal>(query, new
                            {
                               CountryId
                            }).SingleOrDefault();

                            countryName= _objDataCountry.Name;
                        }
                    }


                    query = "SELECT CampaignId, UserId, CampaignTypeId, ProductCategory.Name AS ProductCategory, CASE WHEN ShippingProduct=0 THEN 'Yes' ELSE 'No' END AS ShippingProduct, " +
                            "AboutYourProduct, CampaignTitle, AboutYourBrand, Duration AS CampaignDuration, CASE WHEN PrivateCampaign = 1 THEN 'Yes' ELSE 'N0' END AS PrivateCampaign , Budget.Title AS Budget, " +
                            "FORMAT(Campaign.CreatedDate, 'dd/MM/yyyy ') as CreatedDate, Campaign.Country AS Country " +
                            "FROM  Campaign " +
                            "INNER JOIN CampaignDuration ON Campaign.CampaignDurationId = CampaignDuration.CampaignDurationId " +
                            "INNER JOIN Budget ON Campaign.BudgetId = Budget.BudgetId " +
                            "INNER JOIN ProductCategory ON Campaign.ProductCategoryId = ProductCategory.ProductCategoryId " +
                           // "inner join Country on Country.CountryId=Campaign.CountryId " +
                            "WHERE Status = @Status and Approved=@Approve and Campaign.CampaignDurationId=isnull(@CampaignDurationId,Campaign.CampaignDurationId) and Campaign.BudgetId =isnull(@BudgetId,Campaign.BudgetId) and Campaign.ProductCategoryId= isnull(@ProductCategoryId,Campaign.ProductCategoryId) " +
                             //"and Campaign.CountryId = isnull(@CountryId, Country.CountryId) ORDER BY Campaign.CreatedDate DESC ";
                             "and Campaign.Country like isnull('%'+@Country+'%', Campaign.Country) ORDER BY Campaign.CreatedDate DESC ";

                    List<CampaignModal> _objData = _dbDapperContext.Query<CampaignModal>(query, new
                    {
                        Status = "Publish",
                        Approve=1,
                        ProductCategoryId,
                        BudgetId,
                        CampaignDurationId,
                        //CountryId
                        Country= countryName
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
        public List<SupplementalChannelModal> GetSupplementalChannelList()
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    string query = "SELECT SupplementalId, Path, IsActive FROM SupplementalChannel";
                    List<SupplementalChannelModal> _objData = _dbDapperContext.Query<SupplementalChannelModal> (query).ToList();

                    return _objData;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        public List<YouTubeTypeModal> GetYouTubeTypeList()
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    string query = "SELECT YouTubeVideoTypeId, Title, IsActive FROM YouTubeVideoType";
                    List<YouTubeTypeModal> _objData = _dbDapperContext.Query<YouTubeTypeModal>(query).ToList();

                    return _objData;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        public List<ProjectsModal> GetProjectProposalById(int ProjectId, string UserId, string convertToCurrency)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {

                    string query = @"SELECT Project.ProjectId, Project.ProjectTitle, Project.ProjectDescription, Project.Budget, Project.IsActive, CreatorProfile.FullName AS CreateBy, 
                                    ProjectProposal.ProjectProposalId as ProjectProposalId, ProjectProposal.Status as Status, CurrencyType  
                                    FROM Project INNER JOIN CreatorProfile on CreatorProfile.UserId = Project.CreateBy 
                                    left join  ProjectProposal on  Project.ProjectId=ProjectProposal.ProjectId and ProjectProposal.UserId=@UserId 
                                    Where Project.ProjectId =@ProjectId and IsActive=@IsActive ";
                    List<ProjectsModal> _objData = _dbDapperContext.Query<ProjectsModal>(query, new
                    {
                        UserId = UserId,
                        ProjectId = ProjectId,
                        IsActive = 1

                    }).ToList();

                    foreach (var objList in _objData)
                    {
                        float exchangeRate = CurrencyConverter.GetExchangeRate(objList.CurrencyType, convertToCurrency, 1);
                        if (objList.CurrencyType != null)
                        {
                            double amount = exchangeRate * Convert.ToDouble(objList.Budget);
                            objList.Budget = (amount).ToString("0.##");
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
        public List<BrandMailFile> GetProposalDocumentById(int ProjectId, string userId)
        {
            try
            {
               
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    //#TODO: Get UserId from session or user context
                    // string UserId = "f2363ef0-c455-454c-9aa2-2cd923fb598d";

                    string query = "";

                    query = "Select ProjectProposalId from ProjectProposal where  UserId=@UserId and CampaignId=@CampaignId";
                    var _objData = _dbDapperContext.Query<ProjectsProposalModal>(query, new
                    {
                        UserId = userId,
                        CampaignId = ProjectId
                    }).FirstOrDefault();

                   
                        int projectProposalId = Convert.ToInt32(_objData.ProjectProposalId);

                    //    query = "Select BrandMailId from BrandMail where   ProjectProposalId=@ProjectProposalId and IsDeleted !=@IsDeleted";
                    //    var _objBrandMailData = _dbDapperContext.Query<MailboxModal>(query, new
                    //    {
                    //        //MailFrom = userId,
                    //        ProjectProposalId = projectProposalId,
                    //        IsDeleted = 1
                    //    }).FirstOrDefault();
                    //if (_objBrandMailData != null)
                    //{
                    //    int brandMailId = Convert.ToInt32(_objBrandMailData.BrandMailId);

                        query = "Select BrandMailFileId, BrandMailId, FileName, FilePath From BrandMailFile where BrandMailId =@BrandMailId";
                        List<BrandMailFile> _objFinalData = _dbDapperContext.Query<BrandMailFile>(query, new
                        {
                            BrandMailId = projectProposalId
                        }).ToList();

                        return _objFinalData;
                    //}
                    //else return null;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        public List<CampaignModal> GetAllProposal(string UserId, string convertToCurrency)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    //#TODO: Get UserId from session or user context
                    // string UserId = "f2363ef0-c455-454c-9aa2-2cd923fb598d";

                    string query = "";
                    

                    query = "SELECT Campaign.CampaignId, ProductCategory.Name AS ProductCategory, " +
                      "CampaignTitle,  Duration AS CampaignDuration, CASE WHEN PrivateCampaign = 0 THEN 'Yes' ELSE 'N0' END AS PrivateCampaign , " +
                      "Budget, FORMAT(Campaign.CreatedDate, 'dd/MM/yyyy ') as CreatedDate, " +
                        "Status ,CASE WHEN Campaign.Approved = 1 THEN 'Approved' WHEN Campaign.Rejected = 1 THEN 'Rejected' ELSE 'Under Review' END Approved ,CurrencyType " +
                      "FROM  Campaign " +
                    "INNER JOIN CampaignDuration ON Campaign.CampaignDurationId = CampaignDuration.CampaignDurationId " +
                   // "INNER JOIN Budget ON Campaign.BudgetId = Budget.BudgetId " +
                    "INNER JOIN ProductCategory ON Campaign.ProductCategoryId = ProductCategory.ProductCategoryId " +
                    "WHERE Campaign.UserId= @UserId AND CurrencyType is not null " +
                    "ORDER BY Campaign.CreatedDate DESC";
                    List<CampaignModal> _objData = _dbDapperContext.Query<CampaignModal>(query, new
                    {
                        UserId

                    }).ToList();

                    
                    foreach (var objList in _objData)
                    {
                        float exchangeRate = CurrencyConverter.GetExchangeRate(objList.CurrencyType, convertToCurrency, 1);
                        if (objList.CurrencyType != null)
                        {
                            double amount = exchangeRate * Convert.ToDouble(objList.Budget);
                            objList.Budget = (amount).ToString("0.##");
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

        public int RemoveProposal(int CampaignId, string userId)
        {
            try
            {
                
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = _dbDapperContext.Execute("Delete from Campaign where CampaignId = @CampaignId", new
                    {
                        CampaignId = CampaignId
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

        public int SendProposalUpdate(ProjectProposalUpdateModal objData, string userId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;

                    string query = "";

                   
                            query = @"Insert into ProjectProposalUpdate( Description, UserId, Url, ProjectProposalId) values(@Description,@UserId, @Url,@ProjectProposalId); SELECT CAST(SCOPE_IDENTITY() as int)";
                            var id = _dbDapperContext.Query<int>(query, new
                            {
                               
                                objData.Description,
                                objData.Url,
                                objData.ProjectProposalId,
                                UserId = userId
                              
                            }).Single();


                    query = "SELECT   UserId FROM Campaign where CampaignId=@CampaignId";
                    var _objData1 = _dbDapperContext.Query<CampaignModal>(query, new
                    {
                        objData.CampaignId

                    }).SingleOrDefault();
                   
                        var ClientId = "";

                        ClientId = _objData1.UserId;


                        query = @"Insert into BrandMail(MailTypeId,Subject, Message, CreatedDate, MailFrom, UserId, IsDeleted, IsRead, ProjectProposalUpdateId) values(@MailTypeId,@Subject, @Message, GetUtcDate(),@MailFrom,@UserId, @IsDeleted, @IsRead, @ProjectProposalUpdateId); SELECT CAST(SCOPE_IDENTITY() as int)"; ;
                      var  outputData = _dbDapperContext.Query<int>(query, new
                        {
                            MailTypeId = 4,
                            Subject = "Project Proposal Update",
                            Message = objData.Description,
                            MailFrom = userId,
                            UserId = ClientId,
                            IsDeleted = 0,
                            IsRead = 0,
                          ProjectProposalUpdateId = id
                      }).Single();


                    return output = id;
                       
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return -1;
            }
        }


        public int ProjectProposalUpdatesFile(int id, string fileName, string filePath)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;
                    var query = @"Insert into ProjectProposalUpdatesFile(ProjectProposalUpdateId,FilePath, FileName) values(@ProjectProposalUpdateId,@FilePath, @FileName); SELECT CAST(SCOPE_IDENTITY() as int)";
                    output = _dbDapperContext.Query<int>(query, new
                    {
                        ProjectProposalUpdateId = id,
                        FilePath = filePath,
                        FileName = fileName
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


        public List<CampaignModal> GetCampaignListForAdmin(string UserId, string Type)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    //#TODO: Get UserId from session or user context
                    // string UserId = "f2363ef0-c455-454c-9aa2-2cd923fb598d";
                    List<CampaignModal> _objData = new List<CampaignModal>();
                    string query = "";
                    if (Type == "Pending")
                    {
                        query = "SELECT CampaignId, UserId, CampaignTypeId, ProductCategory.Name AS ProductCategory, CASE WHEN ShippingProduct=0 THEN 'Yes' ELSE 'No' END AS ShippingProduct, " +
                                 "AboutYourProduct, CampaignTitle, AboutYourBrand, Duration AS CampaignDuration, CASE WHEN PrivateCampaign = 1 THEN 'Yes' ELSE 'N0' END AS PrivateCampaign ,  Budget, " +
                                 "FORMAT(Campaign.CreatedDate, 'dd/MM/yyyy ') as CreatedDate , Status, Approved, CurrencyType  " +
                                 "FROM  Campaign " +
                                 "INNER JOIN CampaignDuration ON Campaign.CampaignDurationId = CampaignDuration.CampaignDurationId " +
                                // "INNER JOIN Budget ON Campaign.BudgetId = Budget.BudgetId " +
                                 "INNER JOIN ProductCategory ON Campaign.ProductCategoryId = ProductCategory.ProductCategoryId " +
                                 "WHERE Status <> @Status and (Approved is null or Approved=@Approved) and (Rejected is null or Rejected=@Rejected) " +
                                 "ORDER BY Campaign.CreatedDate DESC";

                         _objData = _dbDapperContext.Query<CampaignModal>(query, new
                        {
                            Status = "Draft",
                            Approved="False",
                            Rejected = "False",

                        }).ToList();
                     //   return _objData;
                    }
                    else  if (Type == "Approved")
                    {
                        query = "SELECT CampaignId, UserId, CampaignTypeId, ProductCategory.Name AS ProductCategory, CASE WHEN ShippingProduct=0 THEN 'Yes' ELSE 'No' END AS ShippingProduct, " +
                                 "AboutYourProduct, CampaignTitle, AboutYourBrand, Duration AS CampaignDuration, CASE WHEN PrivateCampaign = 1 THEN 'Yes' ELSE 'N0' END AS PrivateCampaign ,  Budget, " +
                                 "FORMAT(Campaign.CreatedDate, 'dd/MM/yyyy ') as CreatedDate , Status, Approved ,CurrencyType " +
                                 "FROM  Campaign " +
                                 "INNER JOIN CampaignDuration ON Campaign.CampaignDurationId = CampaignDuration.CampaignDurationId " +
                                // "INNER JOIN Budget ON Campaign.BudgetId = Budget.BudgetId " +
                                 "INNER JOIN ProductCategory ON Campaign.ProductCategoryId = ProductCategory.ProductCategoryId " +
                                 "WHERE Status <> @Status and Approved =@Approved " +
                                 "ORDER BY Campaign.CreatedDate DESC";

                        _objData = _dbDapperContext.Query<CampaignModal>(query, new
                        {
                            Status = "Draft",
                            Approved="True"

                        }).ToList();
                       // return _objData;
                    }
                    else
                    {
                        query = "SELECT CampaignId, UserId, CampaignTypeId, ProductCategory.Name AS ProductCategory, CASE WHEN ShippingProduct=0 THEN 'Yes' ELSE 'No' END AS ShippingProduct, " +
                                 "AboutYourProduct, CampaignTitle, AboutYourBrand, Duration AS CampaignDuration, CASE WHEN PrivateCampaign = 1 THEN 'Yes' ELSE 'N0' END AS PrivateCampaign , Budget, " +
                                 "FORMAT(Campaign.CreatedDate, 'dd/MM/yyyy ') as CreatedDate , Status, CurrencyType " +
                                 "FROM  Campaign " +
                                 "INNER JOIN CampaignDuration ON Campaign.CampaignDurationId = CampaignDuration.CampaignDurationId " +
                                // "INNER JOIN Budget ON Campaign.BudgetId = Budget.BudgetId " +
                                 "INNER JOIN ProductCategory ON Campaign.ProductCategoryId = ProductCategory.ProductCategoryId " +
                                 "WHERE Status <> @Status and Rejected =@Rejected " +
                                 "ORDER BY Campaign.CreatedDate DESC";

                        _objData = _dbDapperContext.Query<CampaignModal>(query, new
                        {
                            Status = "Draft",
                            Rejected = "True"

                        }).ToList();
                      //  return _objData;
                    }

                    foreach (var objList in _objData)
                    {
                        float exchangeRate = CurrencyConverter.GetExchangeRate(objList.CurrencyType, "USD", 1);
                        if (objList.CurrencyType != null)
                        {
                            double amount = exchangeRate * Convert.ToDouble(objList.Budget);
                            objList.Budget = (amount).ToString("0.##");
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


        public List<CreatorModal> GetCreatorList()
        {
            try
            {
                using ( IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    //#TODO: Get UserId from session or user context
                    // string UserId = "f2363ef0-c455-454c-9aa2-2cd923fb598d";

                    string query = "";


                    query = @"SELECT CreatorId, UserId, FullName, ContactNumber, State, CountryId, YouTube, Instagram, Facebook, CategoryId, MinimumBudgetedProject, PastWorkExperience, 
                                Summary, TargetAudience, ProfileImage, DATEDIFF(hour, CreatorProfile.DOB, GETDATE()) / 8766 AS CurrentAge, Language,
                             Categories, Gender FROM CreatorProfile  INNER JOIN AspNetUsers on CreatorProfile.UserId = AspNetUsers.Id";

                    List<CreatorModal> _objData = _dbDapperContext.Query<CreatorModal>(query, new
                    {

                    }).ToList();

                    foreach (var obj in _objData)
                    {
                        if (obj.YouTube != "" && obj.YouTube != null)
                        {
                            var youTubeLink = obj.YouTube.ToString();
                            //  string[] youTubeIds = youTubeLink.Split("/");
                            List<string> youTubeIds = new List<string>(
                                         youTubeLink.Split(new string[] { "/" }, StringSplitOptions.None));
                            if (youTubeIds.Count > 4)
                            {
                                //var client_id = "38f9daf408f9cd0e21662ef453c230a7";// ConfigurationManager.AppSettings["instagram.clientid"].ToString();

                                //////https://api.instagram.com/v1/users/{user-id}/follows?access_token=ACCESS-TOKEN
                                ////https://api.instagram.com/v1/users/search?q=[USERNAME]&client_id=[CLIENT ID]

                                //var instagramUrl = "https://api.instagram.com/v1/users/search?q=gauravranadoon&client_id=" + client_id + "";
                                //// var instagramApi = "AIzaSyDB3tjtbUZNKcraqOhvMMC-HAeJ3yXYvxw";
                                //WebRequest requestInsta = HttpWebRequest.Create(instagramUrl);
                                //requestInsta.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                                //WebResponse responseInsta = requestInsta.GetResponse();
                                //StreamReader readerinsta = new StreamReader(responseInsta.GetResponseStream());
                                //string responseTextInsta = readerinsta.ReadToEnd();
                                /////
                                var youTubeId = youTubeIds[4];
                                var api = "AIzaSyDB3tjtbUZNKcraqOhvMMC-HAeJ3yXYvxw";
                                var url = "https://www.googleapis.com/youtube/v3/channels?part=statistics&id=" + youTubeId + "&key=" + api;
                                WebRequest request = HttpWebRequest.Create(url);
                                request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                                WebResponse response = request.GetResponse();
                                StreamReader reader = new StreamReader(response.GetResponseStream());
                                string responseText = reader.ReadToEnd();
                                dynamic data = JObject.Parse(responseText);
                                obj.YouTube = FormatNumber(Convert.ToInt32(data.items[0].statistics.subscriberCount));
                            }
                            else
                            {
                                obj.YouTube = "NA";
                            }
                        }
                        else
                        {
                            obj.YouTube = "NA";
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
    }
}
