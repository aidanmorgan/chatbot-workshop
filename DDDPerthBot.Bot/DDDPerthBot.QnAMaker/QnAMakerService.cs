using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DDDPerthBot.QnAMaker
{
    [Serializable]
    public class QnAMakerService : IQnAMakerService
    {
        private const string SubscriptionKeyHeader = "Ocp-Apim-Subscription-Key";
        private const string ContentTypeHeader = "Content-Type";
        private const string ContentTypeValue = "application/json";


        private const string QnAMakerBaseUrl = "https://westus.api.cognitive.microsoft.com/qnamaker/v1.0";


        private readonly string _knowledgeBaseId;
        private readonly string _subscriptionKey;
        private readonly string _baseUrl;

        public QnAMakerService(string knowledgeBaseId, string subscriptionKey, string baseUrl = QnAMakerBaseUrl)
        {
            _knowledgeBaseId = knowledgeBaseId;
            _subscriptionKey = subscriptionKey;
            _baseUrl = baseUrl;
        }

        public async Task<QnAResult> ExecuteAsync(string request)
        {
            var qnamakerUriBase = new Uri(_baseUrl);
            var builder = new UriBuilder($"{qnamakerUriBase}/knowledgebases/{_knowledgeBaseId}/generateAnswer");


            //Add the question as part of the body


            //Send the POST request
            using (var client = new WebClient())
            {
                //Set the encoding to UTF8
                client.Encoding = Encoding.UTF8;

                //Add the subscription key header
                client.Headers.Add(SubscriptionKeyHeader, _subscriptionKey);
                client.Headers.Add(ContentTypeHeader, ContentTypeValue);
                var responseString = await client.UploadStringTaskAsync(builder.Uri,
                    JsonConvert.SerializeObject(new QnAMakerRequest(request, 5)));

                try
                {
                    return JsonConvert.DeserializeObject<QnAResult>(responseString);
                }
                catch
                {
                    throw new Exception("Unable to deserialize QnA Maker response string.");
                }
            }
        }
    }
}