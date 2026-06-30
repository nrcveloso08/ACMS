using System;

namespace ATS.DTO.Skills
{
    public class SkillTestResult
    {
        public int Id { get; set; }
        public Guid ApplicantId { get; set; }
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public double Score { get; set; }
        public bool Passed { get; set; }
        public DateTimeOffset TestDate { get; set; }
        public DateTimeOffset? ExpirationDate { get; set; }
        public string Notes { get; set; }
    }
}
