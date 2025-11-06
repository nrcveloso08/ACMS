using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Guatemala
{
    public class GuatemalaDocument
    {
        public int GuatemalaDocument_Id { get; set; }
        public Guid Applicant_Id { get; set; }
        public DateTimeOffset DateUploaded { get; set; }
        public int DocumentType_Id { get; set; }
        public string DocumentType { get; set; }
        public int FileAttachment_Id { get; set; }
        public string MimeType { get; set; }
        public string Contents { get; set; }
    }
}
