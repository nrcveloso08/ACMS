using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Vacancies
{
    public  class Vacancy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VacancyId { get; set; }
        public int JobAdvertisement_Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
