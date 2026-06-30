namespace ATS.DTO.RequisitionRequest
{
    public class RequisitionRequestStatus
    {
        public int RequisitionRequestId { get; set; }
        public int StatusId { get; set; }
        public string Note { get; set; }
        public string CreatedByUser { get; set; }
        public string CreatedByEmail { get; set; }
    }
}
