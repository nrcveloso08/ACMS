using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CsiScheduleController : ControllerBase
    {
        private readonly ICsiScheduleService _csiScheduleService;

        public CsiScheduleController(ICsiScheduleService csiScheduleService)
        {
            _csiScheduleService = csiScheduleService;
        }

        // TODO: GET CSI schedule list / applicants by schedule
        // TODO: PUT check-in management / applicant CSI updates
        // TODO: POST CSI schedule CRUD
    }
}
