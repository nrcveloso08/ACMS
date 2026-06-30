using ATS.DTO.Document;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IResumeRepository
    {
        Task<int> AddResumes(IList<Resume> resumes);
        Task<int> DeleteResume(Guid id);
        Task<IList<Resume>> GetApplicationResumes(Guid applicationId);
        Task<IList<Resume>> GetApplicantResumes(Guid applicantId);
        Task<Resume> GetById(Guid id);
    }

    public class ResumeRepository : IResumeRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public ResumeRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<int> AddResumes(IList<Resume> resumes)
        {
            if (resumes == null)
                throw new ArgumentNullException(nameof(resumes));

            return await _webServiceHelper.PostAsync<IList<Resume>, int>(
                "/v1/Resume/Add",
                resumes
            );
        }

        public async Task<int> DeleteResume(Guid id)
        {
            var parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<int>(
                "/v1/Resume/Delete",
                parameters
            );
        }

        public async Task<Resume> GetById(Guid id)
        {
            var parameters = new NameValueCollection
            {
                { "id", id.ToString() }
            };

            return await _webServiceHelper.GetAsync<Resume>(
                "/v1/Resume/GetById",
                parameters
            );
        }

        public async Task<IList<Resume>> GetApplicationResumes(Guid applicationId)
        {
            var parameters = new NameValueCollection
            {
                { "applicationId", applicationId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<Resume>>(
                "/v1/Resume/GetByApplicationId",
                parameters
            );
        }

        public async Task<IList<Resume>> GetApplicantResumes(Guid applicantId)
        {
            var parameters = new NameValueCollection
            {
                { "applicantId", applicantId.ToString() }
            };

            return await _webServiceHelper.GetAsync<IList<Resume>>(
                "/v1/Resume/GetByApplicantId",
                parameters
            );
        }
    }
}
