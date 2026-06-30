namespace ATS.DTO.Email
{
    public class EmailTemplate
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public TemplateGroupName TemplateGroup { get; set; } = TemplateGroupName.ATS;
        public string EnterpriseModelSource { get; set; }
        public bool IsDeleted { get; set; }
        public string Region { get; set; }
        public int CategoryId { get; set; }
    }

    public enum TemplateGroupName
    {
        ATS = 1,
        KITT = 2,
        KITT_BAXTER = 3,
        KITT_REQUISITIONREQUEST = 4
    }
}
