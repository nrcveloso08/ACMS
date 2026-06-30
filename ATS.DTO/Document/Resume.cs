using System;

namespace ATS.DTO.Document
{
    public class Resume : FileAttachmentBase
    {
        public Guid ApplicantId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
