using ATS.DTO.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.FCRA
{
    public class FCRADocument 
    {
        public int Id { get; set; }
        public int FCRA_Id { get; set; }
        public DateTimeOffset DateUploaded { get; set; }
        public Guid ApplicantId { get; set; }
        public int Document_Id { get; set; }
        public DocumentType Document { get; set; }
        public int Attachment_Id { get; set; }
        
        public FileAttachment File { get; set; }
    }
}
