using ATS.DTO.InterviewSchedule;
using ATS.DTO.PrescreenQuestion;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IPrescreeningQuestionsRepository
    {
        Task<IList<LocalePrescreenQuestion>> GetLocaleQuestions(int locationId);
        Task<IList<LocalePrescreenQuestion>> SaveLocaleQuestion(IList<LocalePrescreenQuestion> localePrescreenQuestions, int locationId);
        Task<bool> SetDeletedPreScreenOptions(int locationId, int[] responseOptions);
        Task<IList<InterviewLocationQuestion>> SaveInterviewLocationQuestion(IList<InterviewLocationQuestion> questions, int locationId);
    }

    public class PrescreeningQuestionsRepository : IPrescreeningQuestionsRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public PrescreeningQuestionsRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<IList<LocalePrescreenQuestion>> GetLocaleQuestions(int locationId)
        {
            var parameters = new NameValueCollection
            {
                { "locationId", locationId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<LocalePrescreenQuestion>>(
                "/v1/Prescreening/GetLocalePrescreenQuestions",
                parameters
            );
        }

        public async Task<IList<LocalePrescreenQuestion>> SaveLocaleQuestion(IList<LocalePrescreenQuestion> localePrescreenQuestions, int locationId)
        {
            if (localePrescreenQuestions == null)
                throw new ArgumentNullException(nameof(localePrescreenQuestions));

            string endpoint =
                $"/v1/Prescreening/SetLocalePrescreenQuestions" +
                $"?locationId={locationId}";

            return await _webServiceHelper.PostAsync<IList<LocalePrescreenQuestion>, IList<LocalePrescreenQuestion>>(
                endpoint,
                localePrescreenQuestions
            );
        }

        public async Task<bool> SetDeletedPreScreenOptions(int locationId, int[] responseOptions)
        {
            if (responseOptions == null)
                throw new ArgumentNullException(nameof(responseOptions));

            string endpoint =
                $"/v1/Prescreening/SetDeletedPreScreenOptions" +
                $"?locationId={locationId}";

            return await _webServiceHelper.PostAsync<int[], bool>(
                endpoint,
                responseOptions
            );
        }

        public async Task<IList<InterviewLocationQuestion>> SaveInterviewLocationQuestion(IList<InterviewLocationQuestion> questions, int locationId)
        {
            if (questions == null)
                throw new ArgumentNullException(nameof(questions));

            string endpoint =
                $"/v1/Interview/SetInterviewLocationQuestions" +
                $"?locationId={locationId}";

            return await _webServiceHelper.PostAsync<IList<InterviewLocationQuestion>, IList<InterviewLocationQuestion>>(
                endpoint,
                questions
            );
        }
    }
}
