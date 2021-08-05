using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.DAL
{
   public class CreatorModal
    {
        public string CurrencyType { get; set; }
      public string IsApproved { get; set; } 
            public int CreatorId { get; set; }
            public string Email { get; set; }
            public string UserId { get; set; }
            public string FullName { get; set; }
            public string ContactNumber { get; set; }
            public string State { get; set; }
            public int CountryId { get; set; }
            public string YouTube { get; set; }
            public string Instagram { get; set; }
            public string Facebook { get; set; }
            public int CategoryId { get; set; }
            public string MinimumBudgetedProject { get; set; }
            public string PastWorkExperience { get; set; }
            public string Summary { get; set; }
            public string TargetAudience { get; set; }
            public string ProfileImage { get; set; }

        public string Categories { get; set; }
        public string DOB { get; set; }
        public string Language { get; set; }

        public string Gender { get; set; }
        public int CurrentAge { get; set; }
    }

    public class CountryModal
    {
        public int CountryId { get; set; }
        public string Name { get; set; }

        public int id { get; set; }
        public string label { get; set; }

    }

    public class CreatorFeedbackModal
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Feedback { get; set; }
    }

    public class ProjectProposalUpdateModal
    {
        public int Id { get; set; }
        public int ProjectProposalId { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string UserId { get; set; }
        public int CampaignId { get; set; }
        public string IsApproved { get; set; }
    }

    public class ProjectProposalUpdatesFileModal
    {
        public int Id { get; set; }
        public int ProjectProposalUpdateId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
       
    }

}
