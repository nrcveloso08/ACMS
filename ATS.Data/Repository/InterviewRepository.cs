using ATS.DTO.InterviewSchedule;
using ATS.Service.UI.Web.DTO.Interview;
using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    public interface IInterviewRepository
    {
        Task<IList<Interview>> GetAllByApplicantId(Guid applicantId);
        Task<Interview> GetLatestByApplicantId(Guid applicantId);
        Task<IList<InterviewLineOfBusinessQuestion>> GetLineOfBusinessQuestions(int lobId = 0);
        Task<IList<InterviewLocationQuestion>> GetLocationQuestions(int locationId = 0);
        Task<bool> Save(Interview interview);
        Task<Interview> GetApplicantResponsesByLocationByLob(Guid applicantId, int locationId, int requisitionId, int lineOfBusinessId);
        Task<bool> SaveByApplicationId(Guid applicationId, InterviewMode interviewMode, string userName, string userEmail);
        Task<IList<Interview>> GetInterviewsByApplicantId(Guid applicantId);
        Task<IList<InterviewLocationQuestion>> SetInterviewLocationQuestions(int locationId, IList<InterviewLocationQuestion> questionsToAdd);
        Task<IList<InterviewLocationQuestion>> GetInterviewLocationQuestionsByLocationId(int locationId = 0);
    }

    public class InterviewRepository : IInterviewRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public InterviewRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IList<Interview>> GetAllByApplicantId(Guid applicantId)
        {
            var parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<Interview>>(
                "/v1/Interview/GetAllByApplicantId",
                parameters
            );
        }

        public async Task<Interview> GetLatestByApplicantId(Guid applicantId)
        {
            var parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<Interview>(
                "/v1/Interview/GetLatestByApplicantId",
                parameters
            );
        }

        public async Task<IList<InterviewLineOfBusinessQuestion>> GetLineOfBusinessQuestions(int lobId = 0)
        {
            var parameters = new NameValueCollection
            {
                { "lobId", lobId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<InterviewLineOfBusinessQuestion>>(
                "/v1/Interview/GetLineOfBusinessQuestions",
                parameters
            );
        }

        public async Task<IList<InterviewLocationQuestion>> GetLocationQuestions(int locationId = 0)
        {
            var parameters = new NameValueCollection
            {
                { "locationId", locationId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<InterviewLocationQuestion>>(
                "/v1/Interview/GetLocationQuestions",
                parameters
            );
        }

        public async Task<bool> Save(Interview interview)
        {
            if (interview == null)
                throw new ArgumentNullException(nameof(interview));

            return await _webServiceHelper.PostAsync<Interview, bool>(
                "/v1/Interview/Save",
                interview
            );
        }

        public async Task<Interview> GetApplicantResponsesByLocationByLob(
            Guid applicantId,
            int locationId,
            int requisitionId,
            int lineOfBusinessId)
        {
            var parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() },
                { "locationId", locationId.ToString() },
                { "requisitionId", requisitionId.ToString() },
                { "lineOfBusinessId", lineOfBusinessId.ToString() }
            };

            return await _webServiceHelper.GetAsync<Interview>(
                "/v1/Interview/GetApplicantResponsesByLocationByLob",
                parameters
            );
        }

        public async Task<bool> SaveByApplicationId(
            Guid applicationId,
            InterviewMode interviewMode,
            string userName,
            string userEmail)
        {
            var endpoint =
                $"/v1/Interview/SaveByApplicationId" +
                $"?applicationId={Uri.EscapeDataString(applicationId.ToString())}" +
                $"&interviewMode={(int)interviewMode}" +
                $"&userName={Uri.EscapeDataString(userName ?? string.Empty)}" +
                $"&userEmail={Uri.EscapeDataString(userEmail ?? string.Empty)}";

            return await _webServiceHelper.PostAsync<object, bool>(
                endpoint,
                null
            );
        }

        public async Task<IList<Interview>> GetInterviewsByApplicantId(Guid applicantId)
        {
            var parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<Interview>>(
                "/v1/Interview/GetInterviewsByApplicantId",
                parameters
            );
        }

        public async Task<IList<InterviewLocationQuestion>> SetInterviewLocationQuestions(
            int locationId,
            IList<InterviewLocationQuestion> questionsToAdd)
        {
            if (questionsToAdd == null)
                throw new ArgumentNullException(nameof(questionsToAdd));

            var endpoint =
                $"/v1/Interview/SetInterviewLocationQuestions" +
                $"?locationId={locationId}";

            return await _webServiceHelper.PostAsync
                <IList<InterviewLocationQuestion>, IList<InterviewLocationQuestion>>(
                    endpoint,
                    questionsToAdd
                );
        }

        public async Task<IList<InterviewLocationQuestion>> GetInterviewLocationQuestionsByLocationId(int locationId = 0)
        {
            var parameters = new NameValueCollection
            {
                { "locationId", locationId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<InterviewLocationQuestion>>(
                "/v1/Interview/GetInterviewLocationQuestionsByLocationId",
                parameters
            );
        }
    }
}