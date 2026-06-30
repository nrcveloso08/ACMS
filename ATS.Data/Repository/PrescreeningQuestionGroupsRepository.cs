using ATS.DTO.PrescreenQuestion;
using ATS.Service.WebServiceHelper;

namespace ATS.Data.Repository
{
    public interface IPrescreeningQuestionGroupsRepository
    {
        Task<PrescreenQuestion> Get(int id);
        Task<LocalePrescreenQuestion> GetLocale(int id);
        Task<PrescreeningQuestionsGroup> GetGroup(int id);
        Task<IList<LocalePrescreenQuestion>> GetAllLocale();
        Task<IList<PrescreenQuestion>> GetAllPrescreenQuestion();
        Task<IList<PrescreeningQuestionsGroup>> GetAllGroups();
        Task<int> Save(PrescreeningQuestionsGroup prescreeningQuestionsGroup);
    }

    public class PrescreeningQuestionGroupsRepository : IPrescreeningQuestionGroupsRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public PrescreeningQuestionGroupsRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public Task<PrescreenQuestion> Get(int id)
        {
            // TODO: Requires DB/business decision - see ATS_BACKEND_ARCHITECTURE.md
            throw new NotImplementedException("Get requires later DB/API decision.");
        }

        public Task<LocalePrescreenQuestion> GetLocale(int id)
        {
            // TODO: Requires DB/business decision - see ATS_BACKEND_ARCHITECTURE.md
            throw new NotImplementedException("GetLocale requires later DB/API decision.");
        }

        public Task<PrescreeningQuestionsGroup> GetGroup(int id)
        {
            // TODO: Requires DB/business decision - see ATS_BACKEND_ARCHITECTURE.md
            throw new NotImplementedException("GetGroup requires later DB/API decision.");
        }

        public Task<IList<LocalePrescreenQuestion>> GetAllLocale()
        {
            // TODO: Requires DB/business decision - see ATS_BACKEND_ARCHITECTURE.md
            throw new NotImplementedException("GetAllLocale requires later DB/API decision.");
        }

        public Task<IList<PrescreenQuestion>> GetAllPrescreenQuestion()
        {
            // TODO: Requires DB/business decision - see ATS_BACKEND_ARCHITECTURE.md
            throw new NotImplementedException("GetAllPrescreenQuestion requires later DB/API decision.");
        }

        public Task<IList<PrescreeningQuestionsGroup>> GetAllGroups()
        {
            // TODO: Requires DB/business decision - see ATS_BACKEND_ARCHITECTURE.md
            throw new NotImplementedException("GetAllGroups requires later DB/API decision.");
        }

        public Task<int> Save(PrescreeningQuestionsGroup prescreeningQuestionsGroup)
        {
            // TODO: Requires DB/business decision - see ATS_BACKEND_ARCHITECTURE.md
            throw new NotImplementedException("Save requires later DB/API decision.");
        }
    }
}
