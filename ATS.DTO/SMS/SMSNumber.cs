namespace ATS.DTO.SMS
{
    public class SMSNumber
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public int? Location_Id { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
