using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Applicant
{
    public class ApplicantPersonalData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string EmailAddress { get; set; }
        public string Position { get; set; }
        public string Availability { get; set; }
        public TypeOfEmployment TypeOfEmployment { get; set; }
        public string TypeOfEmploymentName { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ApartmentNo { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }

        public string Address { get; set; }
    }

    public enum TypeOfEmployment
    {
        Part_Time = 1, Full_Time
    }
}
