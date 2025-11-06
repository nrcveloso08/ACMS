using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO
{
    public class FileSpec
    {
        public string Name { get; set; }

        public string MimeType { get; set; }

        public byte[] Contents { get; set; }

        public FileSpec()
        {
            Name = "";
            MimeType = "";
            Contents = new byte[0];
        }
    }
}
