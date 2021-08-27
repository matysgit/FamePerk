using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FP.DAL.Classes
{
   public class CreateCampaign : ServiceBase
    {

        public int SaveCampaign(CampaignModal objData, string userId, string currencyType)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;

                    string query = "";
                    //return output;

                    if (objData.CampaignId == 0)
                    {
                        
                            query = "Insert into Campaign( UserId, CampaignTypeId, SupplementalChannels, ProductCategoryId, ProductURL, ProductPhoto, ShippingProduct, AboutYourProduct, CampaignTitle, AboutYourBrand, CampaignGoal, "+
                                                           "CampaignDurationId, PrivateCampaign, AudienceAgeId, Budget, CreatedDate, YouTubeVideoType, AudienceGender, Status, Country, CurrencyType )" +
                                    "values( @UserId, @CampaignTypeId, @SupplementalChannels, @ProductCategoryId, @ProductURL, @ProductPhoto, @ShippingProduct, @AboutYourProduct, @CampaignTitle, @AboutYourBrand, @CampaignGoal,"+
                                            "@CampaignDurationId, @PrivateCampaign, @AudienceAgeId, @Budget, getutcDate(), @YouTubeVideoType, @AudienceGender, @Status, @Country, @CurrencyType)";
                        output = _dbDapperContext.Execute(query, new
                        {
                            UserId = userId,
                            objData.CampaignTypeId,
                            objData.SupplementalChannels,
                            objData.ProductCategoryId,
                            objData.ProductURL,
                            objData.ProductPhoto,

                            objData.ShippingProduct,
                            objData.AboutYourProduct,
                            objData.CampaignTitle,
                            objData.AboutYourBrand,
                            objData.CampaignGoal,

                            objData.CampaignDurationId,
                            objData.PrivateCampaign,
                            objData.AudienceAgeId,
                            objData.Budget,
                            //objData.BudgetId,
                            objData.YouTubeVideoType,
                            objData.AudienceGender,
                            objData.Status,
                            objData.Country,
                            CurrencyType= currencyType
                        }); 
                        

                    }
                    else
                    {
                        query = "SELECT   Status FROM Campaign where CampaignId=@CampaignId";

                        var outputStatus = _dbDapperContext.Query<CampaignModal>(query, new
                        {
                            CampaignId = objData.CampaignId
                        }).SingleOrDefault();

                        if(outputStatus.Status == "Send To Platform")
                        {
                            query = "Update  Campaign set Status=@Status FROM Campaign where CampaignId=@CampaignId";

                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Status,
                                CampaignId = objData.CampaignId
                            });

                            return output;
                        }
                        query = "Update Campaign set SupplementalChannels=@SupplementalChannels, ProductCategoryId=@ProductCategoryId, ProductURL=@ProductURL, ProductPhoto=@ProductPhoto, ShippingProduct=@ShippingProduct, AboutYourProduct=@AboutYourProduct, CampaignTitle=@CampaignTitle, AboutYourBrand=@AboutYourBrand, CampaignGoal=@CampaignGoal, " +
                                                              "CampaignDurationId=@CampaignDurationId, PrivateCampaign=@PrivateCampaign, AudienceAgeId=@AudienceAgeId, " +
                                                              "Budget=@Budget,  YouTubeVideoType=@YouTubeVideoType, AudienceGender=@AudienceGender, Status=@Status, " +
                                                              "Country=@Country, CurrencyType=@CurrencyType  where CampaignId=@CampaignId";
                                       
                        output = _dbDapperContext.Execute(query, new
                        {
                           
                            objData.SupplementalChannels,
                            objData.ProductCategoryId,
                            objData.ProductURL,
                            objData.ProductPhoto,

                            objData.ShippingProduct,
                            objData.AboutYourProduct,
                            objData.CampaignTitle,
                            objData.AboutYourBrand,
                            objData.CampaignGoal,

                            objData.CampaignDurationId,
                            objData.PrivateCampaign,
                            objData.AudienceAgeId,
                            //objData.BudgetId,
                            objData.Budget,
                            objData.YouTubeVideoType,
                            objData.AudienceGender,
                            objData.Status,
                            objData.Country,
                            objData.CampaignId,
                            CurrencyType = currencyType

                        });
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

        public int UpdateCampaignById(int CampaignId, string Type, string userId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;

                    string query = "";
                    //return output;
                    query = "Update Campaign set Approved=@Approved, Rejected=@Rejected " +
                                  " where CampaignId=@CampaignId";
                    if (Type == "Approved")
                    {
                        output = _dbDapperContext.Execute(query, new
                        {

                            Approved = "True",
                            Rejected = "False",
                            CampaignId
                        });
                       
                    }
                    else
                    {
                        output = _dbDapperContext.Execute(query, new
                        {

                            Approved = "False",
                            Rejected = "True",
                            CampaignId
                        });
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
    }
}
