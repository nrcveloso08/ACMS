using System.Collections.Generic;

namespace ATS.DTO.FurstPerson
{
    public class FurstPersonReport
    {
        public IList<AssessmentURLRequest> AssessmentURLRequests { get; set; }
        public IList<ScoringBucket> ScoringBucket { get; set; }
        public IList<JobCompletion> JobCompletions { get; set; }
        public IList<FPApplicant> Applicants { get; set; }
    }

    public class AssessmentURLRequest
    {
        public int Completion_Count { get; set; }
        public int Total { get; set; }
        public string Percentage { get; set; }
    }

    public class ScoringBucket
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public string Percentage { get; set; }
    }

    public class JobCompletion
    {
        public string Job_Name { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
        public string Percentage { get; set; }
    }

    public class FPApplicant
    {
        public string Name { get; set; }
        public int Job_Id { get; set; }
        public string Job_Name { get; set; }
        public string First_Contact_Result { get; set; }
        public string First_Screen_Result { get; set; }
        public string First_Scribe_Result { get; set; }
        public string Simulation_Result { get; set; }
        public string Overall_Result { get; set; }
        public string Test_Date { get; set; }
    }
}
