using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DDDPerthBot.QnAMaker
{
    public class QnAMakerRequest
    {
        [JsonProperty(PropertyName = "question")]
        public string Question { get; private set; }

        [JsonProperty(PropertyName = "top")]
        public int Top { get; private set; } = 1;

        public QnAMakerRequest(string question)
        {
            Question = question;
        }

        public QnAMakerRequest(string question, int top)
        {
            Question = question;
            Top = top;
        }
    }
}
