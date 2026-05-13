using ATS.DTO.PrescreenQuestion;

namespace ATS.DTO.EmploymentApplication
{
    public class EmploymentApplicationQuestionResponse
    {
        public int Id { get; set; }
        public string CapturedResponseText { get; set; }
        public PrescreenQuestion.PrescreenQuestion ApplicationQuestions { get; set; }
        public PrescreenQuestionResponseOption SelectedResponse { get; set; }
    }
}
