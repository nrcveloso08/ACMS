using ATS.Service.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Repository
{
    public interface ILanguageRepository
    {
        Task<Dictionary<int, string>> GetAll();
    }

    public class LanguageRepository: ILanguageRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public LanguageRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<Dictionary<int, string>> GetAll()
        {
            return await _webServiceHelper.GetAsync<Dictionary<int, string>>("/v1/Language/GetAll");
        }
    }
}
