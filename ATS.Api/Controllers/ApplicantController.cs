using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantSearchService _applicantService;

        public ApplicantController(IApplicantSearchService applicantService)
        {
            _applicantService = applicantService;
        }

        // TODO: GET applicant by id
        // TODO: GET applicant list/search
        // TODO: POST create or update applicant
    }
}
