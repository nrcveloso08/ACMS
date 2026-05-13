using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.ClientDetail
{
    public class ClientDetail
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public int Location_Id { get; set; }
        public string Location { get; set; }
        public string ClientNavigationTest { get; set; }
        public string ClientWrittedTest { get; set; }
        public string ClientWebsite { get; set; }
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
