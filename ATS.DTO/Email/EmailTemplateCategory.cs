using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Email
{
    public class EmailTemplateCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; } = DateTime.UtcNow;
        public string CreatedByUser { get; set; }
        public string CreatedByEmail { get; set; }
        public bool IsDeleted { get; set; } = false;
        public void SetUserData(string userName, string userEmail)
        {
            CreatedByUser = userName;
            CreatedByEmail = userEmail;
        }
    }
}
