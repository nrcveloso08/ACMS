using System.Collections.Generic;

namespace ATS.DTO.ObeyaBoard
{
    public class ObeyaBoardWaveCard
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int LineOfBusinessId { get; set; }
        public string LineOfBusiness { get; set; }
        public IList<string> Channel { get; set; }
        public string Location { get; set; }
        public string TimeRemaining { get; set; }
        public double TimeRemainingInDays { get; set; }
        public string Wave { get; set; }
        public bool WorkingFromHome { get; set; }
        public int AdjustedHeadCount { get; set; }
        public int InProgressHired { get; set; }
        public int TotalPercentage
        {
            get
            {
                return AdjustedHeadCount == 0 ? 0 : (int)((double)InProgressHired / AdjustedHeadCount * 100);
            }
        }
        public IList<WaveData> WaveData { get; set; }
        public bool DisplayView { get; set; } = true;
        public bool DisplayNotes { get; set; } = true;
    }

    public class WaveData
    {
        public string Language { get; set; }
        public int RequiredFullTime { get; set; }
        public int RequiredPartTime { get; set; }
        public int InProgressFullTime { get; set; }
        public int InProgressPartTime { get; set; }
        public int FullTimePercentage
        {
            get
            {
                return RequiredFullTime == 0 ? 0 : (int)((double)InProgressFullTime / RequiredFullTime * 100);
            }
        }
        public int PartTimePercentage
        {
            get
            {
                return RequiredPartTime == 0 ? 0 : (int)((double)InProgressPartTime / RequiredPartTime * 100);
            }
        }
    }

    public class ApplicantRequisitionAggregate
    {
        public int RequisitionId { get; set; }
        public int LanguageId { get; set; }
        public int ApplicantEmploymentType { get; set; }
        public int Count { get; set; }
    }
}
