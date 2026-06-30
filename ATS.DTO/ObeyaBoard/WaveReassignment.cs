using System;

namespace ATS.DTO.ObeyaBoard
{
    public class WaveReassignment
    {
        public Guid ApplicantId { get; set; }
        public int RequisitionId { get; set; }
        public string CreatedByUser { get; set; }
        public string CreatedByEmail { get; set; }
    }
}
