using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InterviewScheduleController : ControllerBase
    {
        private readonly IInterviewScheduleService _interviewScheduleService;

        public InterviewScheduleController(IInterviewScheduleService interviewScheduleService)
        {
            _interviewScheduleService = interviewScheduleService;
        }

        // TODO: GET interview schedule / appointment slots
        // TODO: GET interview status workflow / modes
        // TODO: POST/PUT schedule appointment
    }
}
