using System;

namespace ATS.DTO.SMS
{
    public class SMSTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Provider { get; set; }
        public string TemplateId { get; set; }
        public int? CategoryId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTimeOffset Created { get; set; } = DateTime.UtcNow;
        public string CreatedByUser { get; set; }
        public string CreatedByEmail { get; set; }
    }

    public class SMSCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ModelSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
