using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DDDPerthBot.QnAMaker
{
    public class QnAResults
    {
        [JsonProperty(PropertyName = "answers")]
        public IList<QnAResult> Answers { get; set; }
    }
}
