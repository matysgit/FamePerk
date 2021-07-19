using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.DAL
{
    public class ProductCategoryModal
    {
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public int ManagedBy { get; set; }
        public string CreatedDate { get; set; }
    }

	public class BudgetModal
    {
        public int BudgetId { get; set; }
        public string Title { get; set; }
        public int ManagedBy { get; set; }
        public string CreatedDate { get; set; }
    }


    public class ClientModal
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Edit { get; set; }
    }

    public class AudienceAgeModal
    {
        public int AudienceAgeId { get; set; }
        public string Title { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public string Duration { get; set; }
        public int ManagedBy { get; set; }
        public string CreatedDate { get; set; }
    }


    public class CampaignTypeModal
    {
        public int CampaignTypeId { get; set; }
        public string Title { get; set; }
        public int ManagedBy { get; set; }
        public string CreatedDate { get; set; }
    }
	
	public class CampaignDurationModal
    {
        public int CampaignDurationId { get; set; }
        public string Duration { get; set; }
        public int ManagedBy { get; set; }
        public string CreatedDate { get; set; }
    }

    public class ChannelModal
    {
        public int ChannelId { get; set; }
        public string Name { get; set; }
        public int ManagedBy { get; set; }
        public string CreatedDate { get; set; }
    }

    public class LevelModal
    {
        public int LevelId { get; set; }
        public string Title { get; set; }
        public string StartRange { get; set; }
        public string EndRange { get; set; }
        public string IsActive { get; set; }
        public string IsDeleted { get; set; }
    }

    public class SupplementalChannelModal
    {
        public int SupplementalId { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string IsActive { get; set; }
        public bool isChecked { get; set; }

    }

    public class YouTubeTypeModal
    {
        public int YouTubeVideoTypeId { get; set; }
        public string Title { get; set; }
        public string IsActive { get; set; }
        public bool isChecked { get; set; }
    }

    //public class CountryModal
    //{
    //    public int Country { get; set; }
    //    public string Name { get; set; }
        
    //}
}
