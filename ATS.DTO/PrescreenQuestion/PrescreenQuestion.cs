namespace ATS.DTO.PrescreenQuestion
{
    public class PrescreenQuestion
    {
        public int Id { get; set; }
        public virtual IList<PrescreenQuestionResponseOption> ResponseOptions { get; set; }
        public string QuestionText { get; set; }
        public virtual PrescreenQuestionDisplayType DisplayType { get; set; }
        public int Order { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsKnockOut { get; set; }
    }
}
