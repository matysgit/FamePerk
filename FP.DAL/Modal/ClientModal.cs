using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.DAL
{
    public class MailboxModal
    {
        public int BrandMailId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string CreatedDate { get; set; }
        public string MailFrom { get; set; }
        public int MailTypeId { get; set; }
        public int IsDeleted { get; set; }
        public string UserId { get; set; }
        public string IsRead { get; set; }
        public string ProjectProposalId { get; set; }

        public int PreviousBrandMailId { get; set; }
        public int ProjectId { get; set; }
        public int CampaignId { get; set; }
        public string PaymentType { get; set; }
        public string NoOfMilestone { get; set; }
        public string ProjectDescription { get; set; }
        public int Milestone { get; set; }
        public string Milestone1 { get; set; }
        public string Milestone2 { get; set; }
        public string Milestone3 { get; set; }
        public string Status { get; set; }
      //  public string UserId { get; set; }
        public string Approved { get; set; }
        public string ProposalAmount { get; set; }
        public string ProposalDate { get; set; }

        public string Milestone1Amount { get; set; }
        public string Milestone2Amount { get; set; }
        public string Milestone3Amount { get; set; }

        public string ProjectProposalUpdateId { get; set; }

         public string Url { get; set; }
        public string ProjectProposal { get; set; }
        public string CurrencyType { get; set; }

    }

    public class ProjectsModal
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public string Budget { get; set; }
        public string CreateBy { get; set; }
        public string IsActive { get; set; }

        // public ICollection<ProjectsProposalModal> ProjectsProposalModal { get; set; } //= new List<ProjectsProposalModal>();
        public string Status { get; set; }
        public string ProjectProposalId { get; set; }
        public string CurrencyType { get; set; }
    }

    public class ProjectsProposalModal
    {
        public int ProjectProposalId { get; set; }
        //public int ProjectId { get; set; }
        public string CurrencyType { get; set; }
        public int CampaignId { get; set; }
        public string PaymentType { get; set; }
        public string NoOfMilestone { get; set; }
        public string ProjectDescription { get; set; }
        public int Milestone { get; set; }
        public string Milestone1 { get; set; }
        public string Milestone2 { get; set; }
        public string Milestone3 { get; set; }

        public string Milestone1Amount { get; set; }
        public string Milestone2Amount { get; set; }
        public string Milestone3Amount { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public string Approved { get; set; }
        public string ProposalAmount { get; set; }
        public string ProposalDate { get; set; }
      
        public int BrandMailId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string CreatedDate { get; set; }
        public string MailFrom { get; set; }
        public int MailTypeId { get; set; }
        public int IsDeleted { get; set; }
       // public string UserId { get; set; }
        public string IsRead { get; set; }
    }

    public class BrandMailFile
    {
      public int  BrandMailFileId { get; set; }
        public int BrandMailId { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public string FilePath { get; set; }

        public int ProjectProposalId { get; set; }
    }

    public class WalletAmountModal
    {
        public string CurrencyType { get; set; }
        public int ID { get; set; }
        public string Amount { get; set; }
        public string UploadDate { get; set; }
        public string UploadedBy { get; set; }

        public string TransactionID { get; set; }
        public string Status { get; set; }
    }

    public class CampaignFillterModal
    {
        public string ProductCategoryId { get; set; }
        public string CampaignDurationId { get; set; }
        public string BudgetId { get; set; }

        public string CountryId { get; set; }

    }

    public class BankDetailModal
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string BankCode { get; set; }
        public string UserId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }

    }

    public class CampaignModal
    {
        public int CampaignId { get; set; }
        public string UserId { get; set; }
        public string CampaignTypeId { get; set; }
        public string SupplementalChannels { get; set; }
        public string ProductCategoryId { get; set; }

        public string ProductCategory { get; set; }
        public string ProductURL { get; set; }
        public string ProductPhoto { get; set; }
        public string ShippingProduct { get; set; }
        public string AboutYourProduct { get; set; }
        public string CampaignTitle { get; set; }
        public string AboutYourBrand { get; set; }
        public string CampaignGoal { get; set; }
        public string CampaignDurationId { get; set; }

        public string CampaignDuration { get; set; }
        public string PrivateCampaign { get; set; }
        public string AudienceAgeId { get; set; }
        public string BudgetId { get; set; }

        public string Budget { get; set; }
        public string CreatedDate { get; set; }
        public string Status { get; set; }

        public string YouTube { get; set; }
        public string YouTubeVideoType { get; set; }
        public string AudienceGender { get; set; }


        public string Approve { get; set; }
        public string Approved { get; set; }
        public string Rejected { get; set; }

      //  public string CountryId { get; set; }
        public string Country { get; set; }

        public string CurrencyType { get; set; }
    }

    public class CreatorFillterModal
    {
        public string CountyId { get; set; }
        public string TargetAudienceId { get; set; }
        

    }

    public class CampaignListModal
    {
        public List<CampaignModal> CampaignList { get; set; }
        public List<CampaignModal> AudList { get; set; }
    }
}
