using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Document
{
    public class FileAttachmentBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public string Contents { get; set; }
        protected bool _isDeleted = false;
        protected DateTimeOffset _modified = DateTimeOffset.Now;
        protected string _modifieBy;

        [Required]
        public bool IsDeleted
        {
            get => _isDeleted;
            set => _isDeleted = value;
        }
        public DateTimeOffset Modified
        {
            get => _modified;
            set => _modified = value;
        }

    }
}
