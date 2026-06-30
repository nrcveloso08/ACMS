using ATS.DTO.Applicant;
using ATS.DTO.CandidateSearch;
using ATS.DTO.LogEntry;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ApplicantRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<Applicant> GetApplicant(Guid id)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<Applicant>(
                "/v1/Applicant/GetApplicant",
                parameters
            );
        }

        public async Task<object> GetApplicants(
            bool? isdeleted = false,
            int draw = 10,
            int start = 10,
            int length = 20,
            string sortColumn = "ApplicationDate",
            string sortDir = "asc",
            string namePartial = "",
            int locationId = 0,
            int jobAdvertisementId = 0)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "isdeleted", isdeleted.ToString() },
                { "draw", draw.ToString() },
                { "start", start.ToString() },
                { "length", length.ToString() },
                { "sortColumn", sortColumn },
                { "sortDir", sortDir },
                { "namePartial", namePartial },
                { "locationId", locationId.ToString() },
                { "jobAdvertisementId", jobAdvertisementId.ToString() }
            };

            return await _webServiceHelper.GetAsync<object>(
                "/v1/Applicant/GetAllByRange",
                parameters
            );
        }

        public async Task<Applicant> AddOrUpdateApplicant(Applicant applicant)
        {
            if (applicant == null)
                throw new ArgumentNullException(nameof(applicant));

            return await _webServiceHelper.PostAsync<Applicant, Applicant>(
                "/v1/Applicant/AddOrUpdateApplicant",
                applicant
            );
        }

        public async Task<List<Applicant>> GetByEmail(string email, int max = 50)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "email", email },
                { "max", max.ToString() }
            };

            return await _webServiceHelper.GetAsync<List<Applicant>>(
                "/v1/Applicant/GetByEmail",
                parameters
            );
        }

        public async Task<List<Applicant>> GetByPhone(string phone, int max = 1)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "phone", phone },
                { "max", max.ToString() }
            };

            return await _webServiceHelper.GetAsync<List<Applicant>>(
                "/v1/Applicant/GetByPhone",
                parameters
            );
        }

        public async Task<IEnumerable<Applicant>> GetByNamePartial(
            string namePartial,
            int max = 50)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "namePartial", namePartial },
                { "max", max.ToString() }
            };

            return await _webServiceHelper.GetAsync<IEnumerable<Applicant>>(
                "/v1/Applicant/GetByNamePartial",
                parameters
            );
        }

        public async Task<int> AddNotes(
            Guid applicantId,
            string note,
            string userName,
            string userEmail)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() },
                { "note", note },
                { "userName", userName },
                { "userEmail", userEmail }
            };

            return await _webServiceHelper.PostAsync<object, int>(
                BuildEndpoint("/v1/Applicant/AddNotes", parameters),
                null
            );
        }

        public async Task<IList<LogEntry>> GetNotes(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<LogEntry>>(
                "/v1/Applicant/GetNotes",
                parameters
            );
        }

        public async Task<LogEntry> GetLatestNote(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<LogEntry>(
                "/v1/Applicant/GetLatestNote",
                parameters
            );
        }

        public async Task<bool> RemoveApplicantPII(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.PostAsync<object, bool>(
                BuildEndpoint("/v1/Applicant/RemoveApplicantPII", parameters),
                null
            );
        }

        public async Task<int> SetApplicantIsSmsEnabledTrue(Guid applicantId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.PostAsync<object, int>(
                BuildEndpoint("/v1/Applicant/SetApplicantIsSmsEnabledTrue", parameters),
                null
            );
        }

        public async Task<CandidateSearchApplicantPageDto> SearchApplicantsByInfo(CandidateSearchApplicantFilters filters)
        {
            if (filters == null)
                throw new ArgumentNullException(nameof(filters));

            return await _webServiceHelper.PostAsync<CandidateSearchApplicantFilters, CandidateSearchApplicantPageDto>(
                "/v1/Applicant/SearchApplicantByInfo",
                filters
            );
        }

        private static string BuildEndpoint(string endpoint, NameValueCollection parameters)
        {
            var queryString = new List<string>();

            foreach (string key in parameters)
            {
                queryString.Add(
                    $"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(parameters[key] ?? string.Empty)}"
                );
            }

            return $"{endpoint}?{string.Join("&", queryString)}";
        }
    }
}