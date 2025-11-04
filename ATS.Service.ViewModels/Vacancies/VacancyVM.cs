using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Service.ViewModels.Vacancies
{
    public  class VacancyVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VacancyId { get; set; }
        public int JobAdvertisement_Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
