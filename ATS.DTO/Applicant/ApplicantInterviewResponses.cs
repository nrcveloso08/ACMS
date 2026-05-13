namespace ATS.DTO.Applicant
{
    public class ApplicantInterviewResponses
    {
        public Guid Id { get; set; }
        public Guid Applicant_Id { get; set; }
        public int PrescreenQuestion_Id { get; set; }
        public PrescreenQuestion.PrescreenQuestion PrescreenQuestion { get; set; }
        public int SelectedResponse_Id { get; set; }
        public string CapturedResponseText { get; set; }
        public int Interview_Id { get; set; }
    }
}
