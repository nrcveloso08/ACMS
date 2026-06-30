namespace ATS.DTO.Reporting
{
    public class AdResponseRate
    {
        public int Location_Id { get; set; }
        public string LocationName { get; set; }
        public string AdSourceName { get; set; }
        public int Responses { get; set; }
        public int Hires { get; set; }
        public decimal ResponseRate { get; set; }
    }
}
