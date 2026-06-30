using System;
using System.Collections.Generic;

namespace ATS.DTO.Skills
{
    public class FurstPersonResultSet
    {
        public int Id { get; set; }
        public Guid ApplicantId { get; set; }
        public string TestName { get; set; }
        public DateTimeOffset CompletedDate { get; set; }
        public IList<FurstPersonResultItem> Results { get; set; }
    }

    public class FurstPersonResultItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
