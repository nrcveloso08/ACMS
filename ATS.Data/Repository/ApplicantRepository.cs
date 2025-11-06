using ATS.DTO.Applicant;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;
        public ApplicantRepository(IWebServiceHelper webServiceHelper) { 
             _webServiceHelper = webServiceHelper;
        }

        public async Task<Applicant> GetApplicant(Guid id)
        {
            NameValueCollection parameters = new NameValueCollection {
                        {"id", id.ToString() }
                    };
            return await _webServiceHelper.GetAsync<Applicant>("/v1/Applicant/GetApplicant", parameters);
        }

        public async Task<object> GetApplicants(bool? isdeleted = false, int draw = 10, int start = 10, int length = 20, string sortColumn = "ApplicationDate", string sortDir = "asc", string namePartial = "", int locationId = 0, int jobAdvertisementId = 0)
        {
            NameValueCollection parameters = new NameValueCollection {
                {"isdeleted", "false" },
                {"draw", draw.ToString() },
                {"start", start.ToString() },
                {"length", length.ToString() },
                {"sortColumn", sortColumn.ToString() },
                {"sortDir", sortDir.ToString() },
                {"namePartial", namePartial.ToString() },
                {"locationId", locationId.ToString() },
                {"jobAdvertisementId", jobAdvertisementId.ToString() }
            };

            object result = await _webServiceHelper.GetAsync<object>("/v1/Applicant/GetAllByRange", parameters);

            return result;
        }
    }
}
