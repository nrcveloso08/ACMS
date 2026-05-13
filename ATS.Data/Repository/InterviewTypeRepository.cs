using ATS.DTO;
using ATS.DTO.InterviewSchedule;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    public interface IInterviewTypeRepository
    {
        Task<IEnumerable<InterviewType>> GetAllInterviewType(bool? includeDeleted = false);
        Task<InterviewType> GetInterviewType(int id);
        Task<int> SaveInterviewType(InterviewType interviewType);
        Task<int> RemoveInterviewType(int id);
    }

    public class InterviewTypeRepository : IInterviewTypeRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public InterviewTypeRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IEnumerable<InterviewType>> GetAllInterviewType(bool? includeDeleted = false)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "includeDeleted", includeDeleted.ToString() }
            };

            return await _webServiceHelper.GetAsync<IEnumerable<InterviewType>>(
                "/v1/InterviewType/GetAllInterviewType",
                parameters
            );
        }

        public async Task<InterviewType> GetInterviewType(int id)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<InterviewType>(
                "/v1/InterviewType/GetInterviewType",
                parameters
            );
        }

        public async Task<int> SaveInterviewType(InterviewType interviewType)
        {
            if (interviewType == null)
                throw new ArgumentNullException(nameof(interviewType));

            return await _webServiceHelper.PostAsync<InterviewType, int>(
                "/v1/InterviewType/SaveInterviewType",
                interviewType
            );
        }

        public async Task<int> RemoveInterviewType(int id)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<int>(
                "/v1/InterviewType/RemoveInterviewType",
                parameters
            );
        }
    }
}