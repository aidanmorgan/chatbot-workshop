using System.Collections.Generic;
using Newtonsoft.Json;

namespace DDDPerthBot.QnAMaker
{
    public class QnAResults
    {
        [JsonProperty(PropertyName = "answers")]
        public IList<QnAResult> Answers { get; set; }
    }
}