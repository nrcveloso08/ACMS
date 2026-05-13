using ATS.DTO.Front;
using ATS.Service.WebServiceHelper;
using System.Collections.Specialized;

namespace ATS.Data.Repository
{
    public interface IFrontApiRepository
    {
        Task<Messages> GetLatestMessages(string email);

        Task<RelatedConversations> GetRelatedConversations(
            string conversationId);

        Task<MessageDetails> GetMessageDetails(string url);
    }

    public class FrontApiRepository : IFrontApiRepository
    {
        private readonly IWebServiceHelper _webServiceHelper;

        public FrontApiRepository(IWebServiceHelper webServiceHelper)
        {
            _webServiceHelper = webServiceHelper;
        }

        public async Task<Messages> GetLatestMessages(string email)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "email", email ?? string.Empty }
            };

            return await _webServiceHelper.GetAsync<Messages>(
                "/v1/FrontApi/Messages/Latest",
                parameters
            );
        }

        public async Task<RelatedConversations> GetRelatedConversations(
            string conversationId)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "conversationId", conversationId ?? string.Empty }
            };

            return await _webServiceHelper.GetAsync<RelatedConversations>(
                "/v1/FrontApi/Messages/Related",
                parameters
            );
        }

        public async Task<MessageDetails> GetMessageDetails(string url)
        {
            NameValueCollection parameters = new NameValueCollection
            {
                { "url", url ?? string.Empty }
            };

            return await _webServiceHelper.GetAsync<MessageDetails>(
                "/v1/FrontApi/Messages/Details",
                parameters
            );
        }
    }
}