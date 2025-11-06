using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.RequisitionRequest
{
    public class RequisitionRequestDetail
    {
        public int Id { get; set; }
        public string EmployeeStatus { get; set; }
        public string LanguageName { get; set; }
        public int RequestedHeadCount { get; set; }
        public double? OverhireAdjustment { get; set; }
        public double? AttritionAdjustment { get; set; }
        public int AdjustedHeadCount { get; set; }
        public int RequisitionRequest_Id { get; set; }
    }
}
