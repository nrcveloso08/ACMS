using System;
using System.Collections.Generic;

namespace ATS.DTO.FollowUp
{
    public class FollowUpApplicantResponseDto
    {
        public Guid ApplicantId { get; set; }
        public string ApplicantName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IList<FollowUpResultDto> FollowUps { get; set; } = new List<FollowUpResultDto>();
    }
}
