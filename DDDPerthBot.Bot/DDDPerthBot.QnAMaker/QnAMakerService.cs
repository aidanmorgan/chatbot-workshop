using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DDDPerthBot.QnAMaker
{
    [Serializable]
    public class QnAMakerService : IQnAMakerService
    {
        private readonly string _knowledgeBaseId;
        private readonly string _subscriptionKey;

        public QnAMakerService(string knowledgeBaseId, string subscriptionKey)
        {
            _knowledgeBaseId = knowledgeBaseId;
            _subscriptionKey = subscriptionKey;
        }

        public async Task<QnAResult> ExecuteAsync(string request)
        {
            Uri qnamakerUriBase = new Uri("https://westus.api.cognitive.microsoft.com/qnamaker/v1.0");
            var builder = new UriBuilder($"{qnamakerUriBase}/knowledgebases/{_knowledgeBaseId}/generateAnswer");


            //Add the question as part of the body
            

            //Send the POST request
            using (WebClient client = new WebClient())
            {
                //Set the encoding to UTF8
                client.Encoding = System.Text.Encoding.UTF8;

                //Add the subscription key header
                client.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
                client.Headers.Add("Content-Type", "application/json");
                var responseString = await client.UploadStringTaskAsync(builder.Uri, JsonConvert.SerializeObject(new QnAMakerRequest(request, 5)));

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
