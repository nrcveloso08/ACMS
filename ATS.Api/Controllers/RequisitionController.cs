using ATS.Service;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RequisitionController : ControllerBase
    {
        private readonly IRequisitionService _requisitionService;

        public RequisitionController(IRequisitionService requisitionService)
        {
            _requisitionService = requisitionService;
        }

        // TODO: GET requisition CRUD / detail rows / audit log / weekly view
        // TODO: GET requisition status lookups / Excel export
    }
}
